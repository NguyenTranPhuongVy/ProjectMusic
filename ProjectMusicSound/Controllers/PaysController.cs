using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoMo;
using Newtonsoft.Json.Linq;
using ProjectMusicSound.Models;

namespace ProjectMusicSound.Controllers
{
    public class PaysController : Controller
    {
        private MusicDataEntities db = new MusicDataEntities();

        // GET: Pays
        public ActionResult Index()
        {
            var pays = db.Pays.Include(p => p.Package).Include(p => p.User);
            return View(pays.ToList());
        }

        public ActionResult ChangeGift()
        {
            return View();
        }

        // GET: Pays/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay pay = db.Pays.Find(id);
            if (pay == null)
            {
                return HttpNotFound();
            }
            return View(pay);
        }

        // GET: Pays/Create
        public ActionResult Create()
        {
            ViewBag.pakage_id = new SelectList(db.Packages, "package_id", "package_name");
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name");
            return View();
        }

        // POST: Pays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pay_id,user_id,pakage_id,pay_datecreate,pay_dateexpiration,pay_summoney")] Pay pay)
        {
            if (ModelState.IsValid)
            {
                db.Pays.Add(pay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pakage_id = new SelectList(db.Packages, "package_id", "package_name", pay.pakage_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", pay.user_id);
            return View(pay);
        }

        // GET: Pays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay pay = db.Pays.Find(id);
            if (pay == null)
            {
                return HttpNotFound();
            }
            ViewBag.pakage_id = new SelectList(db.Packages, "package_id", "package_name", pay.pakage_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", pay.user_id);
            return View(pay);
        }

        // POST: Pays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pay_id,user_id,pakage_id,pay_datecreate,pay_dateexpiration,pay_summoney")] Pay pay)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pakage_id = new SelectList(db.Packages, "package_id", "package_name", pay.pakage_id);
            ViewBag.user_id = new SelectList(db.Users, "user_id", "user_name", pay.user_id);
            return View(pay);
        }

        // GET: Pays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pay pay = db.Pays.Find(id);
            if (pay == null)
            {
                return HttpNotFound();
            }
            return View(pay);
        }

        // POST: Pays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pay pay = db.Pays.Find(id);
            db.Pays.Remove(pay);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        //Thanh toan
        public ActionResult PayPakage(int? id)
        {
            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));
            
            if(id == 2)
            {
                user.v_id = 2;
                db.SaveChanges();


                Pay pay = new Pay
                {
                    pakage_id = id,
                    user_id = user.user_id,
                    pay_datecreate = DateTime.Now,
                    pay_dateexpiration = DateTime.Now.AddDays(30)
                };
                db.Pays.Add(pay);
                db.SaveChanges();
            }
            else if(id == 3)
            {
                user.v_id = 3;
                db.SaveChanges();

                Pay pay = new Pay
                {
                    pakage_id = id,
                    user_id = user.user_id,
                    pay_datecreate = DateTime.Now,
                    pay_dateexpiration = DateTime.Now.AddYears(1)
                };
                db.Pays.Add(pay);
                db.SaveChanges();
            }
            else
            {

            }
            return Redirect("/Users/Profile/" + user.user_id);
        }

        public ActionResult DealVIP(int ? id)
        {
            User user = db.Users.Find(id);
            List<Pay> pays = db.Pays.Where(n => n.user_id == user.user_id).ToList();
            return View(pays);
        }

        public ActionResult ReturnUrl(string errorCode, string amount)
        {
            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));


            if (!errorCode.Equals("0"))
            {
                ViewBag.Mes = "Thanh toán thất bại";
            }
            else
            {
                ViewBag.Mes = "Thanh toán thành công";
                if(amount == "500000")
                {
                    user.v_id = 3;
                    user.user_deadline = DateTime.Now.AddMonths(12);
                }
                else if(amount == "45000")
                {
                    user.v_id = 2;
                    user.user_deadline = DateTime.Now.AddMonths(1);
                }
                else
                {
                    user.v_id = 1;
                }
                
            }
            db.SaveChanges();
            return View();
        }

        public ActionResult PayMomo(int? id)
        {
            HttpCookie httpCookie = Request.Cookies["user_id"];
            User user = db.Users.Find(int.Parse(httpCookie.Value.ToString()));

            Package package = db.Packages.Find(id);

            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMO5RGX20191128";
            string accessKey = "M8brj9K6E22vXoDB";
            string serectkey = "nqQiVSgDMy809JoPF6OzP5OdBUB550Y4";
            string orderInfo = "Thanh toán gói " + package.package_name + " cho tài khoản " + user.user_email;
            string returnUrl = "https://localhost:44328/Pays/ReturnUrl";
            string notifyurl = "https://localhost:44328/Pays/ReturnUrl";

            string amount = package.pakage_price.ToString();
            string orderid = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;


            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };
            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);


            Session["key"] = id;


            return Redirect(jmessage.GetValue("payUrl").ToString());
        }

    }
}
