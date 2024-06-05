using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using yixiaozi.Log.LogToText;

namespace yixiaozi.Net.Email
{
    class EmailUtil
    {
        public static string Sendadress = "";
        public static string Id = "";
        public static string Pwd = "";
        public static string SmptStr = "";

        NetworkCredential evidence;//发件邮箱的登陆凭证
        SmtpClient smpt;//SMTP 事务的主机的名称或 IP 地址
        MailAddress sendadress; //发件地址对象
        MailMessage sendmessage;//邮件对象


        string emailStr = @"([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,5})+"; //邮箱正则表达式对象
        string fileStr = @"^[a-zA-Z]:(((\\(?! )[^/:*?<>\""|\\]+)+\\?)|(\\)?)\s*$";//文件路径正则表达式对象
                                                                                  /// <summary>
                                                                                  /// 邮件标题
                                                                                  /// </summary>
        public string Emailhead { get; set; }
        /// <summary>
        /// 邮件主体信息
        /// </summary>
        public string Emailbody { get; set; }

        public EmailUtil()
        {
            if (!CheckEmailAdress(Sendadress))
                throw new Exception("错误的邮箱地址");
            this.sendadress = new MailAddress(Sendadress);//根据地址字符串生成地址对象

            this.sendmessage = new MailMessage();
            this.sendmessage.From = sendadress;//设置邮件对象的发送地址

            smpt = new SmtpClient(SmptStr);
            smpt.UseDefaultCredentials = true;//使用默认凭据
            smpt.Credentials = new NetworkCredential(Id, Pwd);
            smpt.EnableSsl = true; //启用ssl,也就是安全发送
        }

        /// <summary>
        /// 设置发件邮箱的相关信息
        /// </summary>
        /// <param name="Sendadress">发件地址</param>
        /// <param name="id">SMTP服务登陆账号</param>
        /// <param name="pwd">授权码</param>
        /// <param name="smpt">SMTP 事务的主机的名称或 IP 地址</param>
        public EmailUtil(string Sendadress, string id, string pwd, string smptstr)
        {
            if (!CheckEmailAdress(Sendadress))
                throw new Exception("错误的邮箱地址");
            this.sendadress = new MailAddress(Sendadress);//根据地址字符串生成地址对象

            this.sendmessage = new MailMessage();
            this.sendmessage.From = sendadress;//设置邮件对象的发送地址

            smpt = new SmtpClient(smptstr);
            smpt.UseDefaultCredentials = true;//使用默认凭据
            smpt.Credentials = new NetworkCredential(id, pwd);
            smpt.EnableSsl = true; //启用ssl,也就是安全发送
        }
        /// <summary>
        /// 添加收件人
        /// </summary>
        /// <param name="goaladress">收件地址</param>
        /// <returns></returns>
        public bool AddGoalAdress(string goaladress)
        {
            //验证字符串是否是有效的邮箱地址
            if (!CheckEmailAdress(goaladress))
                return false;
            sendmessage.To.Add(goaladress);
            return true;
        }
        public bool AddFile(string filepath)
        {
            Regex fileReg = new Regex(fileStr);
            //验证字符串是否是有效的文件地址
            if (!fileReg.IsMatch(filepath) || !File.Exists(filepath))
            {
                throw new Exception("错误的文件地址格式或者文件不存在");
            }

            sendmessage.Attachments.Add(new Attachment(filepath.Replace('\\', '/')));
            return true;
        }
        public bool Send()
        {
            if (sendmessage.To.Count == 0)
                return false;
            try
            {
                sendmessage.Subject = Emailhead;
                sendmessage.Body = Emailbody;
                smpt.Send(sendmessage);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                //return false;
            }
        }
        /// <summary>
        /// 验证字符串是否是有效的邮箱地址
        /// </summary>
        /// <param name="address">地址字符串</param>
        /// <returns></returns>
        public bool CheckEmailAdress(string address)
        {
            Regex emailReg = new Regex(emailStr);
            //验证字符串是否是有效的邮箱地址
            return emailReg.IsMatch(address);
        }

        // ==================================================================对外方法

        /// <summary>
        /// 自己对自己发送邮件
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="content"></param>
        public static void CustomeSend(string subject, string content)
        {
            EmailUtil se = new EmailUtil();
            se.AddGoalAdress(Sendadress);
            se.Emailbody = content;
            se.Emailhead = subject;
            se.Send();
        }

        /// <summary>
        /// 发送标准邮件：主题、内容
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="content">内容</param>
        /// <param name="to_splits">收件人（多个使用英文逗号隔开）</param>
        public static void CustomSend(string subject, string content, string to_splits)
        {
            EmailUtil se = new EmailUtil();
            List<string> tos = new List<string>();
            if (to_splits.IndexOf(",") > -1)
            {
                tos = to_splits.Split(',').ToList();
            }
            else
            {
                tos.Add(to_splits);
            }
            foreach (string to in tos)
            {
                se.AddGoalAdress(to);
            }
            se.Emailbody = content;
            se.Emailhead = subject;
            se.Send();
        }

        public static void CustomSend_WithFile(string subject, string content, string to_splits, string filePath)
        {
            EmailUtil se = new EmailUtil();

            string[] tos = to_splits.Split(',');
            foreach (string to in tos)
            {
                se.AddGoalAdress(to);
            }
            se.Emailbody = content;
            se.Emailhead = subject;
            se.AddFile(filePath);
            se.Send();
        }

        /// <summary>
        /// 使用SmtpClient发送电子邮件
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="htmlBody">正文</param>
        /// <returns></returns>
        public static bool HoverTreeSendEmail(string title, string htmlBody)
        {
            bool result = false;
            SmtpClient h_smtpClient = new SmtpClient();
            h_smtpClient.Host = SmptStr;
            MailMessage h_mailMessage = new MailMessage();
            h_mailMessage.From = new MailAddress("");
            string to_splits = "";
            List<string> tos = new List<string>();
            if (to_splits.IndexOf(",") > -1)
            {
                tos = to_splits.Split(',').ToList();
            }
            else
            {
                tos.Add(to_splits);
            }
            foreach (var curuser in tos)
            {
                h_mailMessage.To.Add(curuser);
            }
            //h_mailMessage.To.Add(System.Configuration.ConfigurationManager.AppSettings["mailTo"]);
            h_mailMessage.Subject = title;
            h_mailMessage.Body = htmlBody;
            h_mailMessage.IsBodyHtml = true;

            h_smtpClient.UseDefaultCredentials = false;
            System.Net.NetworkCredential basicAuthenticationInfo =
                    new System.Net.NetworkCredential(Sendadress, Pwd);
            h_smtpClient.Credentials = basicAuthenticationInfo;
            h_smtpClient.EnableSsl = true;
            try
            {
                h_smtpClient.Send(h_mailMessage);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                LogToText.WriteLog("Monitor.Console.Common.Mail.SendMail():" + ex.Message, LogType.Error);
            }
            return result;
        }
    }
}
