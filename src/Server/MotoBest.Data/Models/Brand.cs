namespace MotoBest.Data.Models;

public class Brand : Feature
{
    public Brand()
    {
        Models = new HashSet<Model>();
    }

    public virtual ICollection<Model> Models { get; set; }
}
