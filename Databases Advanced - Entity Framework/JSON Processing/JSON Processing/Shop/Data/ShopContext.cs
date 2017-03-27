namespace Shop.Data
{

    using Models;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class ShopContext : DbContext
    {
        public ShopContext()
            : base("name=ShopContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryProduct> CategoryProducts { get; set; }
        public virtual DbSet<UserFriend> UserFriends { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
             * modelBuilder.Entity<UserFriendship>()
                .HasKey(i => new { i.UserEntityId, i.FriendEntityId });

                modelBuilder.Entity<UserFriendship>()
                .HasRequired(i => i.User)
                .WithMany(i => i.Friends)
                .HasForeignKey(i => i.UserEntityId)
                .WillCascadeOnDelete(true); //the one

                modelBuilder.Entity<UserFriendship>()
                .HasRequired(i => i.Friend)
                .WithMany()
                .HasForeignKey(i => i.FriendEntityId)
                .WillCascadeOnDelete(true); //the one
             * 
             * 
             * */

            modelBuilder.Entity<UserFriend>().HasKey(i => new { i.UserId, i.FriendId });
            modelBuilder.Entity<UserFriend>().HasRequired(c => c.User).WithMany(c=> c.Friends).HasForeignKey(i => i.UserId).WillCascadeOnDelete(false);
            modelBuilder.Entity<UserFriend>().HasRequired(c => c.Friend).WithMany().HasForeignKey(i => i.FriendId).WillCascadeOnDelete(false);

           // modelBuilder.Entity<Product>().HasRequired(c => c.Seller).WithMany().HasForeignKey(i => i.SellerId).WillCascadeOnDelete(false);
           // modelBuilder.Entity<Product>().HasOptional(c => c.Buyer).WithMany().HasForeignKey(i => i.BuyerId).WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }

}