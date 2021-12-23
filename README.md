# Core2Walkthrough_Basic
DIC Tutorial Project to walk through the basics of Core 2 ASP.NET

Dream.In.Code.NET tutorial on Core2 ASP.NET basics.

Link to the tutorial: http://www.dreamincode.net/forums/topic/408395-aspnet-razor-pages-core-2-and-entity-framework-basics/

=================
dreamincode.net tutorial backup ahead of decommissioning

 Posted 26 December 2017 - 02:05 PM 


[u][b]Requirements:[/u][/b]
Visual Studios 2017 Community or higher.

[u][b]Github:[/u][/b]
https://github.com/modi1231/Core2Walkthrough_Basic

There are still a quite a few folk making ASP.NET pages using webforms that are resistant to making the jump to Entity Framework; specifically the new Core 2.  I would highly advocate jumping on the wagon with Core 2 when you start your journey if, for nothing else, Razor pages.  These are a blessing for anyone using Webforms up to 4.0 .NET in terms of getting your bearings, simplifying what needs to be done, and ease of use.

[u][b]Some reading:[/u][/b]

https://docs.microsoft.com/en-us/aspnet/core/getting-started
https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor
https://docs.microsoft.com/en-us/aspnet/entity-framework
https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/intro
https://msdn.microsoft.com/en-us/library/ff798384.aspx
https://blogs.msdn.microsoft.com/dotnet/2017/08/14/announcing-net-core-2-0/
http://www.zdnet.com/article/microsofts-net-core-2-0-whats-new-and-why-it-matters/
https://stackify.com/asp-net-razor-pages-vs-mvc/

[u][b]Background information[/u][/b]
.NET Core 2 helps standardize across platforms, opens up an advanced world of APIs, and is the big step forward past the MVC period.  There is a better setup for file organization, and things are less 'magical' (seems to be the term being batted around).  The Razor page CSHTML and CS code behind pairing really keep home the single functionality concept.  You have a page, and what you do on that page is typically contained inside the code behind.  Not in some other 'controller' file, or stashed elsewhere.  You open up any given page and you should know where to find the functions.  Easy-peasy in my books.  

The important part of Razor pages is they function super similar to an ASPX page.  There's an HTML client side and a CS 'code behind' that should look familiar to most folk.  The trick is Microsoft really opened up how you can tweak, change, add, and modify the page.  You can add in middleware, set up extensions, change routing, and all sorts of complex tasks that would have taken a long path around the bush previously.

With all that customization there comes a huge concern on how does one just make a page work.  How can I show anything to a user, and maybe even shuffle data to a database.  I will concede there is a bit of leg work to get things setup, but once that is done more advanced page additions become a breeze with repetition of concepts and design.

Razor pages also do away with the complex folder structure of MVC.  That always irked me when I was trying to update and old webform and smelled of too much extra work for something that should be simple.  Razor views are all contained in one folder called 'Pages'.  The display view and the code behind CS.  

The concept to keep in mind is this is MVVM.. Model to View to ViewModel.  The way I wrapped my head around that was the concept that your Model generates on a page load.  This is then given to the View (the client page) to work with on render.  the View then gives back a View Model on a post for the code behind to interact with.  Yeah yeah there maybe someone who complains about that mangled description but I kept that flow of events in my head when learning and it kept me orientated correctly.

Much like MVC .NET Razor pages use the '@' sign EVERYWHERE in the View to get handles on models, C# code (like IF statements, FOR loops, etc) usage, and so on.  If you know a bit of MVC, or look for help, it should work similar across the board.

[u][i]Read:[/i][/u] [url="https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor"]Razor Syntax[/url]

The only "magical" area that tripped me up were the use of "Tag Helpers" and naming multiple functions on post.  

Typically if you were to have only one Post on a page you wouldn't need a tag helper and your post function would look like this:

[code]public async Task<IActionResult> OnPostAsync()[/code]
... and your form's post's submit button would look like this:
[code]<input type="submit" value="Add" />[/code]

If you were to have multiple post options - be it with grid editing, adding data, deleting data, etc you just tweak each function name to add the 'asp-page-handler''s value and you are set.

Two different forms's submits.. 
[code]      <button type="submit" asp-page-handler="Delete">Delete</button>

      <input asp-page-handler="Add" type="submit" value="Add" />
[/code]

Their corresponding post functions.  See how the asp-page-handler information is added?  ASP.NET now knows where to find corresponding functions for post actions!
[code]
        public async Task<IActionResult> OnPostDeleteAsync()
{
..
}

        public async Task<IActionResult> OnPostAddAsync()
{
..
}[/code]

[u][b]Outline[/u][/b]
For this tutorial a few things are going to be accomplished.  

First setting up a project.  I'll borrow the setup for the default project, cut back on some of the cruft, and we should have a good platform to add our own functionlity.

Second, I will add an 'in memory' database based off a simple user class.  This DB will reside ONLY in memory and is recreated each time the project starts.  A little magical, but for a quick example works extremely well.

Third, I will show how to get data from this data table, dispaly it, and add to it.  Makin the full rounds from the Model to the View, and the View back with the ViewModel.

[u][b]Tutorial[/u][/b]

[b]1.[/b]
Start by opening up Visual Studios 2017.  
Go to File -> New Project -> Visual C# -> Web.. and click 'ASP.NET Core Web App.  I happen to be on the .NET 4.6.1, but you future folk may have something newer.

I give it a name, and a folder location.

[img]https://i.imgur.com/1yOYtzq.jpg[/img]

Clicking Ok brings up a new project template.  Make sure the drop downs are set to .NET Core, and ASP.NET Core 2.0.

Click 'web application', and then ok.

[img]https://i.imgur.com/eLyB32A.jpg[/img]

Here is the basic template.  You can certainly hit F5 and start it to see what is happening.  If you have telemetry insights on this may take a few moments longer.

[b]2.[/b]
From here delete the contact and error pages.. we don't need those.

[img]https://i.imgur.com/0MmhCgK.jpg[/img]

Double clicking on the 'index' should open up the HTML side.  Delete everything below the first six lines.  I don't need it, and it will just get in the way.

[img]https://i.imgur.com/lW2gzQq.jpg[/img]

You can go a step further into the _layout and remove the nav bar line for the contact page as well.  The _layout is something applied to all pages so if you need a footer, added javascript references, header info, menus, etc they should all go here.  

[img]https://i.imgur.com/4OTirIw.jpg[/img]

Great.. from here everything should still compile nicely.  Let's dig into getting data to and from our database!

[b]3.[/b]
To start we need to add the application's database context.  This will be omni-present for the project and will be the center point of pointing the project to the right tables for CRUD operations.

Right click on the project in the Solution Explorer and click add -> new -> class.  Call it 'AppDbContext'.

Make sure it inherits DBContext, add the required Using, and a few basic functions. 

[img]https://i.imgur.com/I55BGBK.jpg[/img]

[code]using Core2Walkthrough_Basic.Data;
using Microsoft.EntityFrameworkCore;

namespace Core2Walkthrough_Basic
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
[/code]

[b]4.[/b]
Jump over to the 'startup.cs'.  This is the key point where you can add the middle ware, extra functionality, and all sorts of bells and whistles to your project.  For our needs we will add a service to start up in the 'configure services' function.  This will setup an 'in memory database' to be used, and tie to to our AppDbContext.

[img]https://i.imgur.com/w1U3rm1.jpg[/img]

[code]services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestMemoryDB"));[/code]

[b]5.[/b]
Save all of that, and now create a folder called 'Data'.  Inside create a class called 'USERS'.  This should be a fairly one to one representation of any database.  ASP.NET will us this as the 'create table' plans for an in memory database to hold our test data while running.

Give it a few properties, save it, and we are done with the database part and setup work in general.

[img]https://i.imgur.com/V54Hm2R.jpg[/img]

[code]using System;
using System.ComponentModel.DataAnnotations;
 
/*
 * About - Basic class to be made into an in memory db for the example.
 */
namespace Core2Walkthrough_Basic.Data
{
    public class USERS
    {
        public int ID { get; set; }

        [Display(Name = "Name")]
        public string NAME { get; set; }

        public DateTime DATE_ENTERED { get; set; }

    }
}[/code]


The [Display(Name = "Name")] is called a 'data annotation'.  Data annotations are great helpers to standardized your information across pages.  In this case any label that needs to display a name for our property 'name' will use what is inside the "".  It could be "Nam123e" and that is what would be shown.  If nothing is there then it uses the property name.  Sometimes that is ok, and sometimes you want something more user friendly.

https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.displayattribute.name(v=vs.110).aspx

[b]6[/b]
Before we can get the page to display some data, head back to the AppDbContext and add one line for a property to link the DbContext to the USER data class.


[code]        // The bridge between the db and data object to send data to.
        public DbSet<USERS> USER_DBSet { get; set; }
[/code]

[b]7.[/b]
Let's get the data displayed and some user functionality built!

Expand the index.cshtml and open up the cshtml.CS code behind.

First things first - add a readonly variable for the database context.  If I need DB interaction this is always my first line.

Add a message to display to the user on a good save, a list of users to give to the View to display, a single instance of our user class to hold the data from the View's Model on the way back from post, and that's it.

Again.. the list goes TO the client to be rendered.  The BindProperty means on post anything added in there survives the journey back to be acted on.  In this case a save.

Easy so far.. a handle to our DB context, a string to show messages, a list to give to the page on render, and an object to get user info on the way back.  This comprises the 'index model'.

Add our constructor to set the DB context, and the 'on get async' does our first database interaction.  

Its only job is to get ALL the data from the context's USER and stash it in the list to be given to the client to render.  One.. single.. line.

The post is a little more complex.  It checks if the model, as a whole, is valid.  Gives a date to our single object back from the journey, adds it to the db context, which then saves it to the table in memory.

If all goes well a happy message is displayed, and the page is refreshed.. which means the 'on get async' happens, all the data is loaded (even the new), and is shown back to the client.

Pretty slick!

[code]
namespace Core2Walkthrough_Basic.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        [TempData]
        public string Message { get; set; }// no private set b/c we need data back

        public IList<USERS> UserList { get; private set; }// private set so don't need to have data back.. just to show.

        [BindProperty] // survive the journey _BACK_ from the user.
        public USERS UserNew { get; set; }// private not set because the data is needed back

        //Constructor
        public IndexModel(AppDbContext db)
        {
            //Get the database context so we can move data to and from the tables.
            _db = db;
        }

        //When the page is being fetched, load some data to be rendered.
        public async Task OnGetAsync()
        {
            UserList = await _db.USER_DBSet.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Snag the current date time.
            UserNew.DATE_ENTERED = DateTime.Now;

            //Add it to the News database set.
            _db.USER_DBSet.Add(UserNew);

            //Update it.
            await _db.SaveChangesAsync();

            //Inform the user of much success.
            Message = $"User added!";

            //Send it back to the admin page.
            return RedirectToPage();
        }
    }
}
[/code]

[b]8.[/b]
Open up that CSHTML page and lets get some displaying to happen.

Again, remember everything needs the @ sign to reference the model handed to the view.

At the top we can show any messages from the model.

Setup a table to display our data, and use a for loop to cycle though our list and display what is there.

Finally have a form post setup with a single label for a name and a submit.  The submit will trigger a post and anything in that textbox should be added to our in memory database.

Notice the label.  This will go back to our USER data class and check for any 'data annotations'.  If there is something to be had for 'Display' this will use that text instead of the property name.

[code]
@page
@model IndexModel
@{
    ViewData["Title"] = "Home page";
}
<h3><font color="lime"> @Model.Message</font></h3>

@* Table to display the data *@
<table class="table">
    <thead>
        <tr>
            <th>ID</th>
            <th>Name</th>
            <th>DATE_ENTERED</th>
        </tr>
    </thead>
    <tbody>
        @* Common for loop being used to help display HTML! *@
        @foreach (var temp in Model.UserList)
        {
            <tr>
                <td>@temp.ID </td>
                <td>@temp.NAME</td>
                <td>@temp.DATE_ENTERED</td>
            </tr>
        }
    </tbody>
</table>

@*Entry for the user input*@ 
<form method="post">
    <label asp-for="UserNew.NAME"></label>
    <input asp-for="UserNew.NAME" />

    <input type="submit" value="Add" />
</form>
[/code]

[b]9.[/b]
When it runs the grid shows no data from our in memory DB, but if when type in a name and click 'add' that changes!

[img]https://i.imgur.com/Tivea7B.jpg[/img]

[img]https://i.imgur.com/7afgMSg.jpg[/img]

[img]https://i.imgur.com/5gMxmyh.jpg[/img]

Congratulations.. you have a functioning Core 2 Razor project that shuffles data from a database to the client to render, and accepts posted data back to the database.  


An intermediate challenge would be to expand functionality, explore sessions, setup a connection to an existing database, and see about using custom queries instead of our DB context.


[url="http://www.dreamincode.net/forums/topic/408585-aspnet-razor-pages-and-core-2-advanced-1-of-2/"] ASP.NET - Razor Pages and Core 2. Advanced. 1 of 2 [/url]
[url="http://www.dreamincode.net/forums/topic/408586-aspnet-razor-pages-and-core-2-advanced-2-of-2/"]ASP.NET - Razor Pages and Core 2. Advanced. 2 of 2 [/url]
