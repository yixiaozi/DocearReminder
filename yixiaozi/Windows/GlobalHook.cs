﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yixiaozi.Windows
{
    public abstract class GlobalHook
    {

        #region Windows API Code

        [StructLayout(LayoutKind.Sequential)]
        protected class KeyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        protected static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        protected static extern int UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        protected static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("user32")]
        protected static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

        [DllImport("user32")]
        protected static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        protected static extern short GetKeyState(int vKey);

        protected delegate int HookProc(int nCode, int wParam, IntPtr lParam);

        protected const int WH_KEYBOARD_LL = 13;

        protected const int WM_KEYDOWN = 0x100;
        protected const int WM_KEYUP = 0x101;

        protected const int WM_SYSKEYDOWN = 0x104;
        protected const int WM_SYSKEYUP = 0x105;

        protected const byte VK_SHIFT = 0x10;

        protected const byte VK_LSHIFT = 0xA0;
        protected const byte VK_RSHIFT = 0xA1;

        protected const byte VK_LCONTROL = 0xA2;
        protected const byte VK_RCONTROL = 0x3;

        protected const byte VK_LALT = 0xA4;
        protected const byte VK_RALT = 0xA5;

        #endregion

        #region 私有变量
        protected int _hookType;
        protected int _handleToHook;
        protected bool _isStarted;
        protected HookProc _hookCallback;
        #endregion

        #region Properties
        public bool IsStarted
        {
            get
            {
                return _isStarted;
            }
        }
        #endregion

        #region Constructor
        public GlobalHook()
        {
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }
        #endregion

        #region Methods
        public void Start()
        {
            if (!_isStarted && _hookType != 0)
            {
                // Make sure we keep a reference to this delegate!
                // If not, GC randomly collects it, and a NullReference exception is thrown
                _hookCallback = new HookProc(HookCallbackProcedure);
                _handleToHook = SetWindowsHookEx(_hookType, _hookCallback, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
                // Were we able to sucessfully start hook?
                if (_handleToHook != 0)
                {
                    _isStarted = true;
                }
            }
        }

        public void Stop()
        {
            if (_isStarted)
            {
                UnhookWindowsHookEx(_handleToHook);
                _isStarted = false;
            }
        }

        protected virtual int HookCallbackProcedure(int nCode, Int32 wParam, IntPtr lParam)
        {
            // This method must be overriden by each extending hook
            return 0;
        }

        protected void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (_isStarted)
            {
                Stop();
            }
        }
        #endregion
    }
    public class Data
    {

        public string[] nameZh = {
            "退格","Tab","回车","大写锁","Esc",
            "空格",
            "PageUp","PageDown","End","Home",
            "左","上","右","下",
			// 14
			"插入","删除",
			// 16
			"0","1","2","3","4","5","6","7","8","9",
            "右括号","感叹号","@","井号","$","百分号","^","&","星号","左括号",
			// 36
			"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
			// 62
			"Win","App",
			// 64
			"F1","F2","F3","F4","F5","F6","F7","F8","F9","F10","F11","F12",
			// 76
			"左Shift","右Shift","左Ctrl","右Ctrl","左Alt","右Alt",
			// 82
			"分号","等于号","逗号","横线(减号)","点","斜杠","`","左中括号","反斜杠","右中括号","单引号",
			// 93
			"冒号","加号","小于号","下划线","大于号","问号","~","左花括号","|","右花括号","双引号",
			// 104
			"复制","粘贴","撤销","保存","剪切"
        };

        public string[] name = {
			// 0
			"Back","Tab","Enter","Caps","Esc",
			// 5
			"Space",
            "PageUp","PageDown","End","Home",
            "Left","Up","Right","Down",
			// 14
			"Insert","Delete",
			// 16
			"0","1","2","3","4","5","6","7","8","9",
            ")","!","@","#","$","%","^","&","*","(",
			// 36
			"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
			// 62
			"Win","App",
			// 64
			"F1","F2","F3","F4","F5","F6","F7","F8","F9","F10","F11","F12",
			// 76
			"LShift","RShift","LCtrl","RCtrl","LAlt","RAlt",
			// 82
			";","=",",","-",".","/","`","[","\\","]","'",
			// 93
			":","+","<","_",">","?","~","{","|","}","\"",
			// 104
			"Ctrl+C","Ctrl+V","Ctrl+Z","Ctrl+S","Ctrl+X"
        };
        // 存放按键次数的数组
        public int[] times;

        public Data()
        {
            loadData();
        }


        public void loadData()
        {
            return;
            times = new int[name.Length];
            // 从本地的文件读取数据
            string path = Path.Combine((Application.StartupPath + @"\"), "data.csv");
            try
            {
                if (!File.Exists(path)) return;
                //using StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None));
                //for (int i = 0; i < name.Length; i++)
                //{
                //    // 一次读一行数据
                //    string line = reader.ReadLine();
                //    string[] sp = line.Split('\t');
                //    times[i] = int.Parse(sp[1]);
                //}
            }
            catch (Exception e)
            {
                // 读取遇到问题就从上一次保存的文件恢复数据（我可不想辛辛苦苦记录的数据说没就没了）
                File.Delete(path);
                if (File.Exists(path + ".bak"))
                {
                    File.Move(path + ".bak", path);
                    // 重新载入数据
                    loadData();
                    MessageBox.Show("载入数据时出错，尝试从备份文件中恢复\n" + e.Message, "错误");
                }
                else
                {
                    MessageBox.Show("载入数据时出错!\n" + e.Message, "错误");
                }
            }
        }
        // 保存记录的数据
        public void saveData()
        {
            string path = Path.Combine((Application.StartupPath + @"\"), "data.csv");
            try
            {
                // 把之前保存的数据备份
                if (File.Exists(path))
                {
                    if (File.Exists(path + ".bak")) File.Delete(path + ".bak");
                    File.Copy(path, path + ".bak");
                }
                using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None)))
                {
                    for (int i = 0; i < name.Length; i++)
                    {
                        // 一行的数据格式为：名称 + Tab + 按键次数
                        writer.Write(name[i]);
                        writer.Write('\t');
                        writer.Write(times[i].ToString());
                        writer.Write("\r\n");
                    }
                }
            }
            catch (Exception e)
            {
                if (File.Exists(path)) File.Delete(path);
                MessageBox.Show("保存数据时出错!\n" + e.Message, "错误");
            }
        }
    }
    public class KeyboardHook : GlobalHook
    {
        // 储存按键次数的类
        public Data data;

        public KeyboardHook(Data data)
        {
            this.data = data;
            _hookType = WH_KEYBOARD_LL;
        }

        // 监听的回调方法
        protected override int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode > -1)
            {
                KeyboardHookStruct keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                // 只需要监听按下事件
                if (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN)
                {
                    // 获取按键的代码
                    int code = keyboardHookStruct.vkCode;
                    // 根据按键代码让对应数据的数据+1
                    // 只要合理设计一下数组就能大幅减少if-else的数量
                    // 根据code算出数组储存的位置
                    if (code < 32)
                    {
                        switch (code)
                        {
                            case 8: data.times[0]++; break;// back
                            case 9: data.times[1]++; break;// tab
                            case 13: data.times[2]++; break;// enter
                            case 20: data.times[3]++; break;//caps
                            case 27: data.times[4]++; break;// esc
                        }
                    }
                    else if (code < 41)
                    {
                        // 方向键区域
                        data.times[code - 27]++;
                    }
                    else if (code > 47 && code < 65)
                    {
                        // 如果shift也是按下状态的话数据的就是数字上面的符号
                        if (GetKeyState(VK_LSHIFT) + GetKeyState(VK_RSHIFT) != 0) data.times[code - 22]++;
                        // 0-9
                        else data.times[code - 32]++;
                    }
                    else if (code > 64 && code < 91)
                    {
                        // a-z
                        // 如果此时ctrl也是按下状态
                        if (GetKeyState(162) != 0)
                        {
                            // ctrl 组合键
                            if (code == 67) data.times[104]++;
                            else if (code == 86) data.times[105]++;
                            else if (code == 90) data.times[106]++;
                            else if (code == 83) data.times[107]++;
                            else if (code == 88) data.times[108]++;
                        }
                        else data.times[code - 29]++;
                    }
                    else if (code > 95 && code < 106)
                    {
                        // 小键盘0-9
                        data.times[code - 80]++;
                    }
                    else if (code < 112)
                    {
                        // 不常用的键和小键盘符号
                        switch (code)
                        {
                            case 45: data.times[14]++; break;// ins
                            case 46: data.times[15]++; break;// del
                            case 91: data.times[62]++; break; // win
                            case 93: data.times[63]++; break;// apps
                            case 106: data.times[34]++; break;// 乘
                            case 107: data.times[94]++; break;// 加
                            case 109: data.times[85]++; break;// 减
                            case 110: data.times[86]++; break;// 小数点
                            case 111: data.times[87]++; break;// 除
                        }
                    }
                    else if (code < 124)
                    {
                        // F1-F12
                        data.times[code - 48]++;
                    }
                    else if (code > 159 && code < 166)
                    {
                        //left right shift  left right ctrl
                        data.times[code - 84]++;
                    }
                    else if (code > 185 && code < 193)
                    {
                        // ;=,-./`[\]'
                        if (GetKeyState(VK_LSHIFT) + GetKeyState(VK_RSHIFT) != 0) data.times[code - 93]++;
                        else data.times[code - 104]++;
                    }
                    else if (code > 218 && code < 223)
                    {
                        if (GetKeyState(VK_LSHIFT) + GetKeyState(VK_RSHIFT) != 0) data.times[code - 119]++;
                        else data.times[code - 130]++;
                    }
                }
            }
            return CallNextHookEx(_handleToHook, nCode, wParam, lParam);
        }
    }
    public class MouseHelper
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        //移动鼠标
        public const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起
        public const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
    }
}
