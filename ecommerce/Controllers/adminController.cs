using ecommerce.DB;
using ecommerce.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;



namespace ecommerce.Controllers
{
    public class adminController : Controller
    {
        ecommerceEntities db = new ecommerceEntities();
        // GET: admin
        [HttpGet]

        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(tbl_admn avm)
        {
            var ad = db.tb1_administer.Where(x => x.ad_username == avm.ad_username && x.ad_password == avm.ad_password).FirstOrDefault();
            if (ad != null)
            {

                Session["ad_id"] = ad.ad_id.ToString();
                return RedirectToAction("create");

            }
            else
            {
                ViewBag.error = "Invalid username or password";

            }

            return View();
        }

        public ActionResult create()
        {

            if (Session["ad_id"] == null)
            {
                return RedirectToAction("login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult create(tb1_category cvm, HttpPostedFileBase imgfile)
        {
            string path = Uploadimgfile(imgfile);
            if(path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                tbl_category cat = new tbl_category();
                cat.cat_name = cvm.cat_name;
                cat.cat_image = path;
                cat.cat_fk_ad =Convert.ToInt32(Session["ad_id"].ToString());
                db.tbl_category.Add(cat);
                db.SaveChanges();
                return RedirectToAction("viewcategory");//after adding the new category the page will be directed to the new page i.e view category
            }
            return View();
        }

public ActionResult viewcategory(int? page)
{
    int pagesize = 9, pageindex = 1;
    pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
    var list = db.tbl_category.OrderByDescending(x=>x.cat_id).ToList();//whenever the new category is added it will added on the top
    IPagedList<tbl_category> stu = list.ToPagedList(pageindex, pagesize);

    return View(stu);
}


            public string Uploadimgfile(HttpPostedFileBase File)
            {


            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (File != null && File.ContentLength > 0)
            {
                string extension = Path.GetExtension(File.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {

                        path = Path.Combine(Server.MapPath("~/Content/upload"), random + Path.GetFileName(File.FileName)); File.SaveAs(path);
                        path = "/Content/upload/" + random + Path.GetFileName(File.FileName);
                        ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('only jpg,jpeg or png formats are acceptable....');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file');</script>");
                path = "-1";
            }
            return path;

        }
    }
}