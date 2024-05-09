using ecommerce.DB;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ecommerce.Controllers
{
    public class userController : Controller
    {
        ecommerceEntities db = new ecommerceEntities();

        public ActionResult Index(int ?page)
        {
            int pagesize = 7, pageindex = 1;
            pageindex = page.HasValue ? Convert.ToInt32(page) : 1;
            var list = db.tbl_category.OrderByDescending(x => x.cat_id).ToList();//whenever the new category is added it will added on the top
            IPagedList<tbl_category> stu = list.ToPagedList(pageindex, pagesize);

            return View(stu);
        }



-

        public ActionResult signup()
        {
            return View();
        }








        [HttpPost]
  public ActionResult signup(tb1_user1 uvm, HttpPostedFileBase imgfile)
        {
            string path = Uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                tb1_user1 u = new tb1_user1();
                u.u_name = uvm.u_name;
                u.u_email = uvm.u_email;
                u.u_password = uvm.u_password;
                u.u_image = path;
                u.u_contact = uvm.u_contact;
                db.tb1_user1.Add(u);
                db.SaveChanges();
                return RedirectToAction("userlogin");

            }
                return View();
        }










        public ActionResult userlogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult userlogin(tb1_user1 avm)
        {
            tb1_user1 ad = db.tb1_user1.Where(x => x.u_email == avm.u_email && x.u_password == avm.u_password).FirstOrDefault();
            if (ad != null)
            {

                Session["u_id"] = ad.u_id.ToString();//session of the user  will be different to that of admin
                return RedirectToAction("createad");

            }
            else
            {
                ViewBag.error = "Invalid username or password";

            }

            return View();
        }








        public ActionResult createad()
        {
            List<tbl_category> li = db.tbl_category.ToList();
            ViewBag.categorylist = new SelectList(li, "cat_id", "cat_name");
            return View();
        }





        [HttpPost]
        public ActionResult createad(tb1_product pvm, HttpPostedFileBase imgfile)
        {

            string path = Uploadimgfile(imgfile);
            if (path.Equals("-1"))
            {
                ViewBag.error = "Image could not be uploaded....";
            }
            else
            {
                tb1_product p = new tb1_product();
                p.pro_name = pvm.pro_name;
                p.pro_price = pvm.pro_price;
                p.pro_des = pvm.pro_des;
                p.pro_image = path;
                p.pro_fk_cat = pvm.pro_fk_cat;
                p.pro_fk_user = Convert.ToInt32(Session["u_id"].ToString());
                db.tb1_product.Add(p);
                db.SaveChanges();
                Response.Redirect("Index");

            }

            return View();
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