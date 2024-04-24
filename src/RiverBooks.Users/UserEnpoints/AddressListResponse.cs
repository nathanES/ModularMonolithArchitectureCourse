namespace RiverBooks.Users.UserEnpoints;

public class AddressListResponse
{
    public List<UserAddressDto> Addresses { get; set; } = new();
}