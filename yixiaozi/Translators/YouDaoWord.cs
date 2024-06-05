using System;
using System.Collections.Generic;

namespace yixiaozi.Translators
{
  public class YouDaoWord
  {
    public YouDaoWord(string word) => this.Word = !string.IsNullOrWhiteSpace(word) ? word : throw new ArgumentNullException(nameof (word));

    public bool IsEmpty => this.Paraphrase.Count == 0;

    public string Word { get; }

    public List<string> Paraphrase { get; } = new List<string>();

    public List<string> Variant { get; } = new List<string>();

    public List<YouDaoPhonetic> Phonetic { get; } = new List<YouDaoPhonetic>();
  }
}
