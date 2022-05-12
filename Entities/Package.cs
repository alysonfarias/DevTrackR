namespace DevTrackr.API.Entities
{
  public class Package
  {
    public Package(string title, decimal weight)
    {

      Code = Guid.NewGuid().ToString();
      // PackageId = packageId;
      Title = title;
      Weight = weight;
      Delivered = false;
      PostedAt = DateTime.Now;
      Updates = new List<PackageUpdate>();
    }
    public void AddUpdate(string status, bool delivered)
    { 
      if(Delivered){
        throw new Exception("Package has already been delivered");
      }
      var update = new PackageUpdate(status, Id);
      Updates.Add(update);
      Delivered = delivered;
    }

    public int PackageId { get; private set; }
    public int Id { get; private set; }
    public string Code { get; private set; }
    public string Title { get; private set; }
    public decimal Weight { get; private set; }
    public bool Delivered { get; private set; }
    public DateTime PostedAt { get; private set; }
    public List<PackageUpdate> Updates { get; private set; }


  }
}