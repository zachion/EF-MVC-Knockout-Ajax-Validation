using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SolutionName.DataLayer;
using SolutionName.Model;
using SolutionName.Web.ViewModels;

namespace SolutionName.Web
{
    public class SalesController : Controller
    {
        private SalesContext _salesContext;

        public SalesController()
        {
            _salesContext = new SalesContext();
        }


        // GET: Sales
        public ActionResult Index()
        {
            return View(_salesContext.SalesOrders.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel();
            salesOrderViewModel.SalesOrderId = salesOrder.SalesOrderId;
            salesOrderViewModel.CustomerName = salesOrder.CustomerName;
            salesOrderViewModel.PONumber = salesOrder.PONumber;
            salesOrderViewModel.MessageToClient = "I originate from the viewmodel, rather than the model.";


            return View(salesOrderViewModel);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel();
            salesOrderViewModel.ObjectState = ObjectState.Added;

            return View(salesOrderViewModel);
        }


        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }


            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel();
            salesOrderViewModel.SalesOrderId = salesOrder.SalesOrderId;
            salesOrderViewModel.CustomerName = salesOrder.CustomerName;
            salesOrderViewModel.PONumber = salesOrder.PONumber;
            salesOrderViewModel.MessageToClient = string.Format("The original value of Customer Name is {0}.",salesOrderViewModel.CustomerName);
            salesOrderViewModel.ObjectState = ObjectState.Unchanged;

            return View(salesOrderViewModel);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = _salesContext.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }

            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel();
            salesOrderViewModel.SalesOrderId = salesOrder.SalesOrderId;
            salesOrderViewModel.CustomerName = salesOrder.CustomerName;
            salesOrderViewModel.PONumber = salesOrder.PONumber;
            salesOrderViewModel.MessageToClient = string.Format("You are about to permanently delete this sales order");
            salesOrderViewModel.ObjectState = ObjectState.Deleted;

            return View(salesOrderViewModel);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _salesContext.Dispose();
            }
            base.Dispose(disposing);
        }


        public JsonResult Save(SalesOrderViewModel salesOrderViewModel)
        {
            SalesOrder salesOrder = new SalesOrder();
            salesOrder.SalesOrderId = salesOrderViewModel.SalesOrderId;
            salesOrder.CustomerName = salesOrderViewModel.CustomerName;
            salesOrder.PONumber = salesOrderViewModel.PONumber;
            salesOrder.ObjectState = salesOrderViewModel.ObjectState;

            _salesContext.SalesOrders.Attach(salesOrder);
            _salesContext.ChangeTracker.Entries<IObjectWithState>().Single().State = Helpers.ConvertState(salesOrder.ObjectState);
            _salesContext.SaveChanges();

            if (salesOrder.ObjectState == ObjectState.Deleted)
                return Json(new { newLocation = "/Sales/Index/" });

            switch (salesOrderViewModel.ObjectState)
            {
                   
                case ObjectState.Added:
                    salesOrderViewModel.MessageToClient = string.Format("{0}' sales order has been added to the database", salesOrder.CustomerName);
                    break;
                case ObjectState.Modified:
                    salesOrderViewModel.MessageToClient = string.Format("The customer name for this order has been updated to {0} in the database.", salesOrder.CustomerName);
                    break;

            }
            salesOrderViewModel.SalesOrderId = salesOrder.SalesOrderId;
            salesOrderViewModel.ObjectState = ObjectState.Unchanged;
            
            return Json(new { salesOrderViewModel });
        }
    }
}
