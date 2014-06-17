namespace max.web
{
  public delegate string ICreateRedirectUrls<in Output>(IContainRequestInfo request , Output model);
}