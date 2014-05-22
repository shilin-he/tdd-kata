namespace max.web
{
  public interface IDisplayInformation
  {
    string render<ViewModel>(ViewModel view_model);
  }
}