
namespace Shop.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    [Table("UserFriends")]
    public class UserFriend
    {
        [InverseProperty("User")]
        [Key, Column(Order = 0)]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Friend")]
        public int FriendId { get; set; }

        public virtual User Friend { get; set; }

        public virtual User User { get; set; }

        
    }
}
