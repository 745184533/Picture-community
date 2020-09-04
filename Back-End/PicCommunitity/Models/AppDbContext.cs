using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicCommunitity.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<blog> blog { get; set; }
        public DbSet<checkInfo> checkInfo { get; set; }
        public DbSet<checks> checks { get; set; }
        public DbSet<commentLike> commentLike { get; set; }
        public DbSet<favoritePicture> favoritePicture { get; set; }
        public DbSet<follow> follow { get; set; }
        public DbSet<likespicture> likesPicture { get; set; }
        public DbSet<orderInfo> orderInfo { get; set; }
        public DbSet<orderPic> orderPic { get; set; }
        public DbSet<orders> orders { get; set; }
        public DbSet<ownBlog> ownBlog { get; set; }
        public DbSet<ownTag> ownTag { get; set; }
        public DbSet<users> users { get; set; }
        public DbSet<payment> payment { get; set; }
        public DbSet<picComment> picComment { get; set; }
        public DbSet<picture> picture { get; set; }
        public DbSet<publishPicture> publishPicture { get; set; }
        public DbSet<tag> tag { get; set; }
        public DbSet<TotalInfo> TotalInfo { get; set; }
        public DbSet<userInfo> userInfo { get; set; }
        public DbSet<wallet> wallet { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<checks>().HasKey(t => new { t.u_id, t.c_id });
            modelBuilder.Entity<commentLike>().HasKey(t => new{ t.u_id,t.p_id,t.comm_u_id});
            modelBuilder.Entity<favoritePicture>().HasKey(t => new { t.u_id, t.p_id });
            modelBuilder.Entity<follow>().HasKey(t => new { t.fans_id,t.follow_id });
            modelBuilder.Entity<likespicture>().HasKey(t => new { t.u_id,t.p_id });
            modelBuilder.Entity<orderPic>().HasKey(t => new { t.o_id, t.p_id });
            modelBuilder.Entity<orders>().HasKey(t => new { t.u_id, t.o_id });
            modelBuilder.Entity<ownBlog>().HasKey(t => new { t.u_id, t.b_id });
            modelBuilder.Entity<ownTag>().HasKey(t => new { t.p_id, t.tag_name });
            modelBuilder.Entity<payment>().HasKey(t => new { t.u_id, t.pay_time });

            modelBuilder.Entity<picComment>().HasKey(t => new { t.u_id, t.p_id});

            modelBuilder.Entity<publishPicture>().HasKey(t => new { t.u_id, t.p_id });

            base.OnModelCreating(modelBuilder);
        }

    }
}

//CREATE TABLE users 
//(u_id  varchar(8) primary key,
//u_password varchar(10),
//u_name varchar(20),
//u_type varchar(2),
//u_status varchar(2),
//create_time date
//);

//CREATE TABLE follow
//(fans_id varchar(8),
//follow_id varchar(8),
//primary key(fans_id, follow_id),
//foreign key(fans_id) references users(u_id),
//foreign key(follow_id) references users(u_id)
//);

//CREATE TABLE userInfo
//(u_id varchar(8) primary key,
//u_name varchar(20),
//birthday date,
//phone_number varchar(12),
//mail varchar(40),
//message varchar(50),
//foreign key(u_id) references users(u_id)
//);

//CREATE TABLE wallet 
//(u_id varchar(8) primary key,
//fund int,
//coin int,
//publish_num int,
//buy_num int,
//foreign key (u_id) references users(u_id)
//);

//CREATE TABLE payment
//(u_id varchar(8),
//pay_time date,
//money int,
//source_typepicture varchar(4),
//foreign key(u_id) references users(u_id),
//primary key(u_id, pay_time)
//);

//CREATE TABLE blog
//(b_id varchar(8) primary key,
//b_date date,
//b_type varchar(5),
//b_text varchar(100)
//);

//CREATE TABLE ownBlog
//(u_id varchar(8),
//b_id varchar(8),
//primary key(u_id, b_id),
//foreign key(u_id) references users(u_id),
//foreign key(b_id) references blog(b_id)
//);

//CREATE TABLE picture
//(p_id varchar(8) primary key,
//p_width int,
//p_height int,
//p_info varchar(50),
//p_url varchar(100),
//p_status varchar(2),
//likes int,
//dislikes int,
//comm_num int
//);

//CREATE TABLE publishPicture
//(u_id varchar(8),
//p_id varchar(8),
//publish_time date,
//primary key(u_id, p_id),
//foreign key(u_id)references users(u_id),
//foreign key(p_id)references picture(p_id)
//);

//CREATE TABLE favoritePicture
//(u_id varchar(8),
//p_id varchar(8),
//fav_time date,
//primary key(u_id, p_id),
//foreign key(u_id)references users(u_id),
//foreign key(p_id)references picture(p_id)
//);

//CREATE TABLE likesPicture
//(u_id varchar(8),
//p_id varchar(8),
//like_time date,
//like_type varchar(2),
//primary key(u_id, p_id),
//foreign key(u_id)references users(u_id),
//foreign key(p_id)references picture(p_id)
//);

//CREATE TABLE checkInfo
//(c_id varchar(8) primary key,
//p_id varchar(8),
//c_status varchar(2),
//pass_num int,
//fail_num int,
//foreign key(p_id)references picture(p_id)
//);

//CREATE TABLE checks
//(u_id varchar(8),
//c_id varchar(8),
//c_time date,
//primary key(u_id, c_id),
//foreign key(u_id) references users(u_id),
//foreign key(c_id) references checkInfo(c_id)
//);

//CREATE TABLE tag
//(tag_name varchar(10) primary key
//);

//CREATE TABLE ownTag
//(p_id varchar(8),
//tag_name varchar(10),
//primary key(p_id, tag_name),
//foreign key(p_id)references picture(p_id),
//foreign key(tag_name)references tag(tag_name)
//);

//CREATE TABLE picComment
//(u_id varchar(8),
//p_id varchar(8),
//p_comment varchar(50),
//likes int,
//primary key(u_id, p_id),
//foreign key(u_id)references users(u_id),
//foreign key(p_id)references picture(p_id)
//);

//CREATE TABLE commentLike
//(u_id varchar(8),
//comm_u_id varchar(8),
//p_id varchar(8),
//primary key(u_id, comm_u_id, p_id),
//foreign key(u_id)references users(u_id),
//foreign key(comm_u_id, p_id)references picComment(u_id, p_id)
//);

//CREATE TABLE orderInfo
//(o_id varchar(8) primary key,
//o_type varchar(4),
//o_status varchar(2),
//o_description varchar(50),
//reward int
//);

//CREATE TABLE orders
//(u_id varchar(8),
//o_id varchar(8),
//primary key(u_id, o_id),
//foreign key(u_id)references users(u_id),
//foreign key(o_id)references orderInfo(o_id)
//);

//CREATE TABLE orderPic
//(o_id varchar(8),
//p_id varchar(8),
//primary key(o_id, p_id),
//foreign key(o_id)references orderInfo(o_id),
//foreign key(p_id)references picture(p_id)
//);

//CREATE TABLE TotalInfo
//(user_num long,
//blog_num long,
//pic_num long,
//check_num long,
//order_num long
//);