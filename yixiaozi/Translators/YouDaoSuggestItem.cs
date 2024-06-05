namespace yixiaozi.Translators
{
  public class YouDaoSuggestItem
  {
    public string Title { get; set; }

    public string Explain { get; set; }

    public int ResultNum { get; set; }

    public override string ToString() => this.Title;
  }
}
