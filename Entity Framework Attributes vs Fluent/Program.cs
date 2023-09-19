using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;

//ref link:https://www.youtube.com/watch?v=xq0Siemsl24&list=PLRwVmtr-pp06bXl6mbwDfK1eW9sAIvWUZ&index=13
// EntityFramework - is a object relational mapping(ORM) data tool
// Nuget Installed --EntityFramework-- Entity Framework 6 (EF6) is a tried and tested object-relational mapper for .NET with many years of feature development and stabilization.
// SQL Server Management Studio - Database app
// SQL Server Profiler - app for tracing SQL Database

// CRUD - Create, Read, Update, Delete


/*  AppConfig
 *  
 *  <?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<configSections>
		<section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.4.4, Culture=neutral"/>
	</configSections>
	<entityFramework>
		<defaultConnectionFactory type="System.Data.Entity.Infrastructure.SqlConnectionFactory, EntityFramework"/>
		<providers>
			<provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer"/>
		</providers>
	</entityFramework>
<connectionStrings>
	<add name="MeContext" connectionString="Data Source=.;Initial Catalog=MyTestDb;Integrated Security=True" providerName="System.Data.SqlClient"/>
</connectionStrings>
</configuration>
 * 
 * 
 * 
 *      ------ SQLServer - New Query CMD --------
 *      use MyTestDb

 *      select *
        from Videos

        select *
        from PlayLists

        select *
        from videoplaylists

        --insert into Videos values ('Me Title', 'Whatever', 5)     // to comment in SQLServer is type --
        --insert into Videos values ('Me Title', 'Whatever', 1)        


        use master          // switch database
 * 
 * 
 */



class PlayList
{
    public int ID { get; set; }
    public string Title { get; set; }
    public List<Video> Videos { get; set; }     // Auto List Mapping table - SQLserver

    //public List<Video> GoodVideos { get; set; }     // Auto List Mapping table - SQLserver
    //public List<Video> BadVideos { get; set; }     // Auto List Mapping table - SQLserver
    //public ICollection<Video> Videos { get; set; }     // ICollection -- also a ManytoMany Relationship
    //public override string ToString()
    //{
    //    string ret = Title + ": ";
    //    foreach (Video vid in Videos)
    //        ret += vid.Title + ", ";
    //    return ret;
    //}
}

class Video
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    [Required]
    public PlayList MyList { get; set; }
    
    
    //public List<PlayList> Playlists { get; set; }       // 

    // Many-to-Manty Relationship -- added navigation
    // ----------------------OneToMany Relationship--- if no List<> here--------------------
    //public List<PlayList> MyPlaylists { get; set; }     // Auto List Mapping table - SQLserver
    //public ICollection<PlayList> MyPlaylists { get; set; }     // ICollection -- also a ManytoMany Relationship
}

class MeContext : DbContext     // PipeLine --SQLServer and VS-- Schema
{
    //public MeContext() : base(@"Data Source=.;Initial Catalog=MyTestDb;username = whateverUsername;password = yadayadayada")
    public MeContext() : base(@"Data Source=.;Initial Catalog=MyTestDb;Integrated Security=True")
    {
    }
    public DbSet<Video> Videos { get; set; }
    public DbSet<PlayList> Playlists { get; set; }

    // -------Describe further for Relation (ManyToMany)

    //protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //{
    //    //modelBuilder.Entity<PlayList>().HasMany(p => p.Videos).WithMany();  // high level metadata -- with lambda navigation
    //    modelBuilder.Entity<PlayList>().HasMany(p => p.GoodVideos).WithMany(v => v.Playlists);  // high level metadata -- with lambda navigation
    //    modelBuilder.Entity<PlayList>().HasMany(p => p.BadVideos).WithMany();  // high level metadata -- with lambda navigation
    //    //modelBuilder.Entity<Video>().HasMany(v => v.Playlists); // error no playlist property
    //}       // recommendation for knowledge in "HOOK Design Pattern "
    /*
     *      ---EntityFramework SQLServer
     * 
     * use MyTestDb

       select *
       from Videos
       
       select *
       from PlayLists
       
       select *
       --from VideoPlayLists
       from PlayListVideos		-- Mapping Table for OnModelCreating
       
       use master
     * 
     * 
     */


}

class MainClass
{
    static void Main()
    {
        MeContext db = new MeContext();
        db.Database.Delete();

        Video goodVid = new Video
        {
            Title = "Me Good Video",
            Description = "Me good description"
        };
        PlayList thePlaylist = new PlayList { Title = "Me Awesome Playlist" };
        thePlaylist.Videos = new List<Video> { };

        db.Playlists.Add(thePlaylist);
        db.Videos.Add(goodVid);
        db.SaveChanges();




        //MeContext db = new MeContext();
        //db.Database.Delete();
        //
        //Video goodVid = new Video
        //{
        //    Title = "Me Good Video",
        //    Description = "Me good description"
        //};
        //PlayList thePlaylist = new PlayList { Title = "Me Awesome Playlist" };
        ////thePlaylist.Videos = new List<Video> { goodVid };
        //thePlaylist.GoodVideos = new List<Video> { goodVid };
        //thePlaylist.BadVideos = new List<Video> { goodVid };
        //
        //db.Playlists.Add(thePlaylist);
        //db.SaveChanges();




        //MeContext db = new MeContext(); // Pipeline to VS and EntityFramework SQLServer app
        //db.Database.Delete();
        //
        //PlayList mePlaylist = new PlayList();
        //mePlaylist.Title = "Entity Framework";
        //PlayList meOtherPlaylist = new PlayList();
        //meOtherPlaylist.Title = "Epicness";
        //
        //Video meAwesomeVideo = new Video
        //{
        //    Title = "The Next Viral Hit",
        //    Description = "Share this with your friends."
        //};
        //
        //mePlaylist.Videos = new List<Video> { meAwesomeVideo };     // Auto List Mapping table - SQLserver
        //meOtherPlaylist.Videos = new List<Video> { meAwesomeVideo };        // Auto List Mapping table - SQLserver
        //
        //db.Playlists.Add(mePlaylist);
        //db.Playlists.Add(meOtherPlaylist);
        //
        //Console.WriteLine(mePlaylist);
        //Console.WriteLine(meOtherPlaylist);
        //
        //db.SaveChanges();





        //MeContext db = new MeContext(); // Pipeline to VS and EntityFramework SQLServer app
        //db.Database.Delete();
        //
        //PlayList mePlaylist = new PlayList();
        //mePlaylist.Title = "Entity Framework";
        //
        //Video meVideo = new Video
        //{
        //    Title = "Hello World Entity Framework",
        //    Description = "Learn the entity framework"
        //};
        //
        //mePlaylist.Videos = new List<Video> { meVideo };
        //db.Playlists.Add(mePlaylist);
        //db.SaveChanges();



        //MeContext db = new MeContext(); // Pipeline to VS and EntityFramework SQLServer app
        //
        //PlayList theList = db.Playlists.Include(list => list.Videos).Single();
        //
        //foreach (Video vid in theList.Videos)
        //    Console.WriteLine(vid.Title);
        //
        ////db.Videos.Select(v => v.Title).ToList().ForEach(Console.WriteLine);
        //return;     // ignore the line of code below
        //
        //db.Database.Delete();
        //Video meVideo = new Video
        //{
        //    Title = "Hello World Entity Framework",
        //    Description = "Learn the entity framework"
        //};
        //db.Videos.Add(meVideo);
        //PlayList mePlaylist = new PlayList();
        //mePlaylist.Title = "Entity Framework";
        //
        //mePlaylist.Videos = new List<Video> { meVideo };
        //db.Playlists.Add(mePlaylist);   // Foreign key relation to ID
        //db.SaveChanges();



        //// CRUD - Create, Read, Update, Delete
        //var meContext = new MeContext();
        //Video vid = meContext.Videos.FirstOrDefault();
        //
        //meContext.Videos.Remove(vid);   // --- Delete ---
        //meContext.SaveChanges();

        //vid.Title = "Beggining CRUD"; // --- Update ---
        //meContext.SaveChanges();


        //Console.WriteLine(vid.Title);   // --- Read ----
        //Console.WriteLine(vid.Description); // --- Read ----

        //Video vid = new Video     // ---- Create ----
        //{
        //    Title = "Entity Framework CRUD Operations",
        //    Description = "Learn the meaning of CRUD."
        //};
        //meContext.Videos.Add(vid);
        //meContext.SaveChanges();




        //var vid = new Video
        //{
        //    Title = "Hello World Entity Framework",
        //    Description = "Learn about the entity framework"
        //};
        //var meContext = new MeContext();


        //Video meVideo = meContext.Videos.Single();    // ----Read----
        //Console.WriteLine(meVideo.ID);
        //Console.WriteLine(meVideo.Title);
        //Console.WriteLine(meVideo.Description);



        //meContext.Database.Delete();  // ----Delete---

        //meContext.Videos.Add(vid);    // ----Create---
        //meContext.SaveChanges();
    }
}