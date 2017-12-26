using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core2Walkthrough_Basic.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
