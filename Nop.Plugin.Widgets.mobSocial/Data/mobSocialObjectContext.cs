﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Nop.Core;
using Nop.Data;
using Nop.Plugin.Widgets.MobSocial.Domain;

namespace Nop.Plugin.Widgets.MobSocial.Data
{

    public class MobSocialObjectContext : DbContext, IDbContext
    {
        public MobSocialObjectContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            this.DropPluginTable<EventPageAttendance>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new EventPageMap());
            modelBuilder.Configurations.Add(new EventPagePictureMap());
            modelBuilder.Configurations.Add(new EventPageHotelMap());
            modelBuilder.Configurations.Add(new EventPageAttendanceMap());


            modelBuilder.Configurations.Add(new BusinessPageMap());
            modelBuilder.Configurations.Add(new BusinessPagePictureMap());
            modelBuilder.Configurations.Add(new BusinessPageCouponMap());


            modelBuilder.Configurations.Add(new GroupPageMap());
            modelBuilder.Configurations.Add(new GroupPageMemberMap());

            modelBuilder.Configurations.Add(new SkateMoveMap());
            modelBuilder.Configurations.Add(new CustomerSkateMoveMap());

            modelBuilder.Configurations.Add(new CustomerFriendMap());

            modelBuilder.Configurations.Add(new TeamPageMap());

            modelBuilder.Configurations.Add(new CustomerAlbumMap());
            modelBuilder.Configurations.Add(new CustomerAlbumPictureMap());


            modelBuilder.Configurations.Add(new CustomerVideoAlbumMap());
            modelBuilder.Configurations.Add(new CustomerVideoMap());
            modelBuilder.Configurations.Add(new CustomerVideoLikeMap());

            modelBuilder.Configurations.Add(new CustomerProfileViewMap());

            modelBuilder.Configurations.Add(new CustomerTimelineMap());

            modelBuilder.Configurations.Add(new PictureTagMap());

            modelBuilder.Configurations.Add(new CustomerProfileMap());

            modelBuilder.Configurations.Add(new NotificationMap());
            



            base.OnModelCreating(modelBuilder);
        }

        public string CreateDatabaseInstallationScript()
        {
            return ((IObjectContextAdapter) this).ObjectContext.CreateDatabaseScript();
        }

        public void Install()
        {
            //It's required to set initializer to null (for SQL Server Compact).
            //otherwise, you'll get something like "The model backing the 'your context name' context has changed since the database was created. Consider using Code First Migrations to update the database"
            Database.SetInitializer<MobSocialObjectContext>(null);

            Database.ExecuteSqlCommand(CreateDatabaseInstallationScript());
            SaveChanges();
        }

        public void Uninstall()
        {
            try
            {
                // uninstall regardless of errors

                // Remove Url Records
                var dbScript = "DELETE FROM UrlRecord WHERE EntityName = 'Customer' OR EntityName = 'EventPage'; ";
                Database.ExecuteSqlCommand(dbScript);

                // DROP Tables
                this.DropPluginTable<EventPageAttendance>();
                this.DropPluginTable<EventPagePicture>();
                this.DropPluginTable<EventPageHotel>();
                this.DropPluginTable<BusinessPage>();
                this.DropPluginTable<BusinessPageCoupon>();
                this.DropPluginTable<BusinessPagePicture>();
                this.DropPluginTable<EventPage>();
                this.DropPluginTable<GroupPageMember>();
                this.DropPluginTable<CustomerTimeline>();
                //this.DropPluginTable"('CustomerAlbumPictureLike>();
                this.DropPluginTable<CustomerProfile>();
                this.DropPluginTable<CustomerAlbumPicture>();
                this.DropPluginTable<CustomerAlbum>();
                this.DropPluginTable<CustomerVideoLike>();
                this.DropPluginTable<CustomerVideo>();
                this.DropPluginTable<CustomerVideoAlbum>();
                this.DropPluginTable<GroupPage>();
                this.DropPluginTable<TeamPage>();
                this.DropPluginTable<CustomerSkateMove>();
                this.DropPluginTable<SkateMove>();
                this.DropPluginTable<CustomerFriend>();
                this.DropPluginTable<PictureTag>();
                this.DropPluginTable<Notification>();
                this.DropPluginTable<CustomerProfileView>();

                SaveChanges();
            }
            catch (Exception)
            {

            }

        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public System.Collections.Generic.IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText,
                                                                                             params object[] parameters)
            where TEntity : BaseEntity, new()
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<TElement> SqlQuery<TElement>(string sql,
                                                                                   params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, bool ensureTransaction,  int? timeout = null, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }
    }

}
