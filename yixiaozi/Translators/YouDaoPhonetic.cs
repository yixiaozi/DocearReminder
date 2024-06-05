using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using yixiaozi.Net.HttpHelp;
using System.Windows.Media;

namespace yixiaozi.Translators
{
  public class YouDaoPhonetic
  {
    private static readonly MediaPlayer MediaPlayer = new MediaPlayer();

    public async Task PlayAsync(string url)
    {
      string str = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Cache", "YouDao");
      if (!Directory.Exists(str))
        Directory.CreateDirectory(str);
      string fn = Path.Combine(str, this.GetHash(url) + ".mp3");
      if (!File.Exists(fn))
      {
        using (Stream ms = await HttpClientHelper.HttpClient.GetStreamAsync(url))
        {
          using (FileStream fs = File.OpenWrite(fn))
            await ms.CopyToAsync((Stream) fs);
        }
      }
      YouDaoPhonetic.MediaPlayer.Open(new Uri(fn));
      YouDaoPhonetic.MediaPlayer.Play();
      fn = (string) null;
    }

    private string GetHash(string input)
    {
      byte[] buffer = Encoding.UTF8.GetBytes(input);
      using (SHA1CryptoServiceProvider cryptoServiceProvider = new SHA1CryptoServiceProvider())
        buffer = cryptoServiceProvider.ComputeHash(buffer);
      return BitConverter.ToString(buffer).Replace("-", string.Empty).ToLower();
    }

    public string Type { get; set; }

    public string Text { get; set; }

    public string Source { get; set; }
  }
}
