
using MyProject.Models.AddressModel;
using MyProject.Models.Cart;
using MyProject.Models.UserModel;
public class Users
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password {  get; set; }
    public string Email { get; set; }

    //latest//
    public bool IsActive { get; set; } = true;
 
    public string Role { get; set; } = "User";
    public ICollection<CartItems>? CartItems { get; set; }
    public ICollection<Address>? Addresses { get; set; }




}
