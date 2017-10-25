using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SolutionName.Model;

namespace SolutionName.Web.ViewModels
{
    public static class Helpers
    {
        public static SalesOrderViewModel CreateSalesOrderViewModelFromSalesOrder(SalesOrder salesOrder)
        {
            SalesOrderViewModel salesOrderViewModel = new SalesOrderViewModel();
            salesOrderViewModel.SalesOrderId = salesOrder.SalesOrderId;
            salesOrderViewModel.CustomerName = salesOrder.CustomerName;
            salesOrderViewModel.PONumber = salesOrder.PONumber;
            salesOrderViewModel.ObjectState = ObjectState.Unchanged;

            foreach (SalesOrderItem salesOrderItem in salesOrder.SalesOrderItems)
            {
                SalesOrderItemViewModel salesOrderItemViewModel = new SalesOrderItemViewModel();
                salesOrderItemViewModel.SalesOrderItemId = salesOrderItem.SalesOrderItemId;
                salesOrderItemViewModel.ProductCode = salesOrderItem.ProductCode;
                salesOrderItemViewModel.Quantity = salesOrderItem.Quantity;
                salesOrderItemViewModel.UnitPrice = salesOrderItem.UnitPrice;

                salesOrderItemViewModel.ObjectState = ObjectState.Unchanged;

                salesOrderItemViewModel.SalesOrderId = salesOrder.SalesOrderId;

                salesOrderViewModel.SalesOrderItems.Add(salesOrderItemViewModel);
            }

            return salesOrderViewModel;
        }


        public static SalesOrder CreateSalesOrderFromSalesOrderViewModel(SalesOrderViewModel salesOrderViewModel)
        {
            SalesOrder salesOrder = new SalesOrder();
            salesOrder.SalesOrderId = salesOrderViewModel.SalesOrderId;
            salesOrder.CustomerName = salesOrderViewModel.CustomerName;
            salesOrder.PONumber = salesOrderViewModel.PONumber;
            salesOrder.ObjectState = salesOrderViewModel.ObjectState;

            int temporarySalesOrderItemId = -1; 

            foreach (SalesOrderItemViewModel salesOrderItemViewModel in salesOrderViewModel.SalesOrderItems)
            {
                SalesOrderItem salesOrderItem = new SalesOrderItem();
                salesOrderItem.ProductCode = salesOrderItemViewModel.ProductCode;
                salesOrderItem.Quantity = salesOrderItemViewModel.Quantity;
                salesOrderItem.UnitPrice = salesOrderItemViewModel.UnitPrice;

                salesOrderItem.ObjectState = salesOrderItemViewModel.ObjectState;

                if (salesOrderItemViewModel.ObjectState != ObjectState.Added)
                    salesOrderItem.SalesOrderItemId = salesOrderItemViewModel.SalesOrderItemId;
                else
                {
                    salesOrderItem.SalesOrderItemId = temporarySalesOrderItemId;
                    temporarySalesOrderItemId--;
                }

                salesOrderItem.SalesOrderId = salesOrderViewModel.SalesOrderId;

                salesOrder.SalesOrderItems.Add(salesOrderItem);
            }

            return salesOrder;
        }


        public static string GetMessageToClient(ObjectState objectState, string customerName)
        {
            string messageToClient = string.Empty;

            switch (objectState)
            {
                case ObjectState.Added:
                    messageToClient = string.Format("A sales order for {0} has been added to the database.", customerName);
                    break;

                case ObjectState.Modified:
                    messageToClient = string.Format("The customer name for this sales order has been updated to {0} in the database.", customerName);
                    break;
            }

            return messageToClient;
        }
    }
}