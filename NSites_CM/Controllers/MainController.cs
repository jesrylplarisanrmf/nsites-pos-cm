using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

using NSites_CM.Models.Generics;
using NSites_CM.Models.Inventorys;
using NSites_CM.Models.POSs;
using NSites_CM.Models.Systems;

using System.Data;
using System.Net.Http.Headers;
using System.Net.Mail;

namespace NSites_CM.Controllers
{
    public class MainController : ApiController
    {
        #region "INITIALIZATION"
        //Generics
        Common loCommon = new Common();
        
        //Inventorys
        Customer loCustomer = new Customer();
        Supplier loSupplier = new Supplier();
        InventoryGroup loInventoryGroup = new InventoryGroup();
        Category loCategory = new Category();
        Stock loStock = new Stock();
        Unit loUnit = new Unit();
        InventoryType loInventoryType = new InventoryType();
        Location loLocation = new Location();
        Inventory loInventory = new Inventory();
        InventoryDetail loInventoryDetail = new InventoryDetail();
        
        //POS
        Cashier loCashier = new Cashier();
        ModeOfPayment loModeOfPayment = new ModeOfPayment();
        Discount loDiscount = new Discount();
        CashierPeriod loCashierPeriod = new CashierPeriod();
        POSTransaction loPOSTransaction = new POSTransaction();
        POSTransactionDetail loPOSTransactionDetail = new POSTransactionDetail();
        ReturnedItem loReturnedItem = new ReturnedItem();

        //Systems
        User loUser = new User();
        UserGroup loUserGroup = new UserGroup();
        SystemConfiguration loSystemConfigurations = new SystemConfiguration();
        AuditTrail loAuditTrail = new AuditTrail();
        #endregion

        #region "GENERICS"
        [HttpGet]
        public string test()
        {
            return "Test Successful!";
        }

        [HttpGet]
        public DataTable getDataFromSearch(string pQueryString)
        {
            return loCommon.getDataFromSearch(pQueryString);
        }

        [HttpGet]
        public DataTable getUserGroupMenuItems(string pUsername)
        {
            return loCommon.getUserGroupMenuItems(pUsername);
        }

        [HttpGet]
        public DataTable getUserGroupRights(string pUsername)
        {
            return loCommon.getUserGroupRights(pUsername);
        }

        [HttpGet]
        public DataTable getMenuItems()
        {
            return loCommon.getMenuItems();
        }

        [HttpGet]
        public DataTable getAllMenuItems()
        {
            return loCommon.getAllMenuItems();
        }

        [HttpGet]
        public DataTable getAllRights(string pItemName)
        {
            return loCommon.getAllRights(pItemName);
        }

        [HttpGet]
        public DataTable getMenuItemsByGroup(string pUserGroupId)
        {
            return loCommon.getMenuItemsByGroup(pUserGroupId);
        }

        [HttpGet]
        public DataTable getEnableRights(string pItemName, string pUserGroupId)
        {
            return loCommon.getEnableRights(pItemName, pUserGroupId);
        }

        [HttpGet]
        public DataTable getEnableCompanys(string pUserGroupId)
        {
            return loCommon.getEnableCompanys(pUserGroupId);
        }

        [HttpGet]
        public bool sendEmail(string pFrom, string pTo, string pCC, string pSubject, string pBody, string pUsername, string pUserPassword)
        {
            return loCommon.sendEmail(pFrom, pTo, pCC, pSubject, pBody, pUsername, pUserPassword);
        }

        [HttpGet]
        public DataTable getTemplateNames(string pMenuName, string pUserId)
        {
            return loCommon.getTemplateNames(pMenuName, pUserId);
        }

        [HttpGet]
        public DataTable getTemplateName(string pId)
        {
            return loCommon.getTemplateName(pId);
        }

        [HttpGet]
        public DataTable getSearchFilters(string pTemplateId)
        {
            return loCommon.getSearchFilters(pTemplateId);
        }

        [HttpGet]
        public string insertSearchTemplate(string pTemplateName, string pItemName, string pPrivate, string pUserId)
        {
            return loCommon.insertSearchTemplate(pTemplateName, pItemName, pPrivate, pUserId);
        }

        [HttpGet]
        public bool removeSearchFilter(string pTemplateId)
        {
            return loCommon.removeSearchFilter(pTemplateId);
        }

        [HttpGet]
        public bool removeSearchTemplate(string pId)
        {
            return loCommon.removeSearchTemplate(pId);
        }

        [HttpGet]
        public bool renameSearchTemplate(string pId, string pTemplateName)
        {
            return loCommon.renameSearchTemplate(pId, pTemplateName);
        }

        [HttpGet]
        public bool updateSearchTemplate(string pId, string pTemplateName, string pItemName, string pPrivate)
        {
            return loCommon.updateSearchTemplate(pId, pTemplateName, pItemName, pPrivate);
        }

        [HttpGet]
        public bool insertSearchFilter(string pTemplateId, string pField, string pOperator, string pValue, string pCheckAnd, string pCheckOr, int pSequence)
        {
            return loCommon.insertSearchFilter(pTemplateId, pField, pOperator, pValue, pCheckAnd, pCheckOr,pSequence);
        }

        [HttpGet]
        public DataTable getViewDetails()
        {
            return loCommon.getViewDetails();
        }

        [HttpGet]
        public DataTable getStoredProcedureDetails(string pDatabaseName)
        {
            return loCommon.getStoredProcedureDetails(pDatabaseName);
        }

        [HttpGet]
        public DataTable getFunctionDetails(string pDatabaseName)
        {
            return loCommon.getFunctionDetails(pDatabaseName);
        }

        [HttpGet]
        public DataTable getTableDetails()
        {
            return loCommon.getTableDetails();
        }

        [HttpGet]
        public DataTable getMenuItemDetails()
        {
            return loCommon.getMenuItemDetails();
        }

        [HttpGet]
        public DataTable getItemRightDetails()
        {
            return loCommon.getItemRightDetails();
        }

        [HttpGet]
        public DataTable getSystemConfigurationDetails()
        {
            return loCommon.getSystemConfigurationDetails();
        }

        [HttpGet]
        public DataTable getNextTabelSequenceId(string pDescription)
        {
            return loCommon.getNextTabelSequenceId(pDescription);
        }
        #endregion "END OF GLOBAL"

        #region "INVENTORYS"
        #region "Customer"
        [HttpGet]
        public DataTable getCustomers(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loCustomer.getCustomers(pDisplayType, pPrimaryKey, pSearchString);
        }

        [HttpGet]
        public DataTable getCustomerDefault()
        {
            return loCustomer.getCustomerDefault();
        }

        [HttpGet]
        public DataTable getCustomerCreditLimit(string pCustomerId)
        {
            return loCustomer.getCustomerCreditLimit(pCustomerId);
        }

        [HttpPost]
        public string insertCustomer([FromBody]Customer pCustomer)
        {
            return loCustomer.insertCustomer(pCustomer);
        }

        [HttpPost]
        public string updateCustomer([FromBody]Customer pCustomer)
        {
            return loCustomer.updateCustomer(pCustomer);
        }

        [HttpGet]
        public bool removeCustomer(string pId, string pUserId)
        {
            return loCustomer.removeCustomer(pId, pUserId);
        }
        #endregion ""

        #region "Supplier"
        [HttpGet]
        public DataTable getSuppliers(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loSupplier.getSuppliers(pDisplayType, pPrimaryKey, pSearchString);
        }

        [HttpPost]
        public string insertSupplier([FromBody]Supplier pSupplier)
        {
            return loSupplier.insertSupplier(pSupplier);
        }

        [HttpPost]
        public string updateSupplier([FromBody]Supplier pSupplier)
        {
            return loSupplier.updateSupplier(pSupplier);
        }

        [HttpGet]
        public bool removeSupplier(string pId, string pUserId)
        {
            return loSupplier.removeSupplier(pId, pUserId);
        }
        #endregion ""

        #region "Inventory Group"
        [HttpGet]
        public DataTable getInventoryGroups(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loInventoryGroup.getInventoryGroups(pDisplayType, pPrimaryKey, pSearchString);
        }

        [HttpPost]
        public string insertInventoryGroup([FromBody]InventoryGroup pInventoryGroup)
        {
            return loInventoryGroup.insertInventoryGroup(pInventoryGroup);
        }

        [HttpPost]
        public string updateInventoryGroup([FromBody]InventoryGroup pInventoryGroup)
        {
            return loInventoryGroup.updateInventoryGroup(pInventoryGroup);
        }

        [HttpGet]
        public bool removeInventoryGroup(string pId, string pUserId)
        {
            return loInventoryGroup.removeInventoryGroup(pId, pUserId);
        }
        #endregion ""

        #region "Category"
        [HttpGet]
        public DataTable getCategorys(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loCategory.getCategorys(pDisplayType, pPrimaryKey, pSearchString);
        }

        [HttpPost]
        public string insertCategory([FromBody]Category pCategory)
        {
            return loCategory.insertCategory(pCategory);
        }

        [HttpPost]
        public string updateCategory([FromBody]Category pCategory)
        {
            return loCategory.updateCategory(pCategory);
        }

        [HttpGet]
        public bool removeCategory(string pId, string pUserId)
        {
            return loCategory.removeCategory(pId, pUserId);
        }
        #endregion ""

        #region "Stock"
        [HttpGet]
        public DataTable getStocks(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loStock.getStocks(pDisplayType, pPrimaryKey, pSearchString);
        }

        [HttpGet]
        public DataTable getStocksByCode(string pCode)
        {
            return loStock.getStocksByCode(pCode);
        }

        [HttpGet]
        public DataTable getSaleableStocks()
        {
            return loStock.getSaleableStocks();
        }

        [HttpGet]
        public DataTable getSaleableStock(string pCode, string pDescription)
        {
            return loStock.getSaleableStock(pCode, pDescription);
        }

        [HttpGet]
        public DataTable getStockCard(DateTime pFromDate, DateTime pToDate, string pStockId, string pLocationId)
        {
            return loStock.getStockCard(pFromDate, pToDate, pStockId, pLocationId);
        }

        [HttpGet]
        public DataTable getStockCardBegBal(DateTime pFromDate, string pStockId, string pLocationIld)
        {
            return loStock.getStockCardBegBal(pFromDate, pStockId, pLocationIld);
        }

        [HttpGet]
        public DataTable getStockQtyOnHand(string pLocationId, string pStockId)
        {
            return loStock.getStockQtyOnHand(pLocationId, pStockId);
        }

        [HttpGet]
        public DataTable getReorderLevel()
        {
            return loStock.getReorderLevel();
        }

        [HttpPost]
        public string insertStock([FromBody]Stock pStock)
        {
            return loStock.insertStock(pStock);
        }

        [HttpPost]
        public string updateStock([FromBody]Stock pStock)
        {
            return loStock.updateStock(pStock);
        }

        [HttpGet]
        public bool removeStock(string pId, string pUserId)
        {
            return loStock.removeStock(pId, pUserId);
        }
        #endregion ""

        #region "Unit"
        [HttpGet]
        public DataTable getUnits(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loUnit.getUnits(pDisplayType, pPrimaryKey, pSearchString);
        }

        [HttpPost]
        public string insertUnit([FromBody]Unit pUnit)
        {
            return loUnit.insertUnit(pUnit);
        }

        [HttpPost]
        public string updateUnit([FromBody]Unit pUnit)
        {
            return loUnit.updateUnit(pUnit);
        }

        [HttpGet]
        public bool removeUnit(string pId, string pUserId)
        {
            return loUnit.removeUnit(pId, pUserId);
        }
        #endregion ""

        #region "Inventory Type"
        [HttpGet]
        public DataTable getInventoryTypes(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loInventoryType.getInventoryTypes(pDisplayType, pPrimaryKey, pSearchString);
        }

        [HttpPost]
        public string insertInventoryType([FromBody]InventoryType pInventoryType)
        {
            return loInventoryType.insertInventoryType(pInventoryType);
        }

        [HttpPost]
        public string updateInventoryType([FromBody]InventoryType pInventoryType)
        {
            return loInventoryType.updateInventoryType(pInventoryType);
        }

        [HttpGet]
        public bool removeInventoryType(string pId, string pUserId)
        {
            return loInventoryType.removeInventoryType(pId, pUserId);
        }
        #endregion ""

        #region "Location"
        [HttpGet]
        public DataTable getLocations(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loLocation.getLocations(pDisplayType, pPrimaryKey, pSearchString);
        }

        [HttpPost]
        public string insertLocation([FromBody]Location pLocation)
        {
            return loLocation.insertLocation(pLocation);
        }

        [HttpPost]
        public string updateLocation([FromBody]Location pLocation)
        {
            return loLocation.updateLocation(pLocation);
        }

        [HttpGet]
        public bool removeLocation(string pId, string pUserId)
        {
            return loLocation.removeLocation(pId, pUserId);
        }
        #endregion ""

        #region "Inventory"
        [HttpGet]
        public DataTable getInventorys(string pType, string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loInventory.getInventorys(pType, pDisplayType, pPrimaryKey, pSearchString);
        }

        [HttpGet]
        public DataTable getNextInventoryId()
        {
            return loInventory.getNextInventoryId();
        }

        [HttpGet]
        public DataTable getInventoryStatus(string pId)
        {
            return loInventory.getInventoryStatus(pId);
        }

        [HttpGet]
        public DataTable getStockTransferOut(string pToLocationId, string pSearchString)
        {
            return loInventory.getStockTransferOut(pToLocationId, pSearchString);
        }

        [HttpPost]
        public string insertInventory([FromBody]Inventory pInventory)
        {
            return loInventory.insertInventory(pInventory);
        }

        [HttpPost]
        public string updateInventory([FromBody]Inventory pInventory)
        {
            return loInventory.updateInventory(pInventory);
        }

        [HttpGet]
        public bool removeInventory(string pId, string pUserId)
        {
            return loInventory.removeInventory(pId, pUserId);
        }

        [HttpGet]
        public bool finalInventory(string pId, string pUserId)
        {
            return loInventory.finalInventory(pId, pUserId);
        }

        [HttpGet]
        public bool cancelInventory(string pId, string pCancelledReason, string pUserId)
        {
            return loInventory.cancelInventory(pId, pCancelledReason, pUserId);
        }
        #endregion ""

        #region "Inventory Detail"
        [HttpGet]
        public DataTable getInventoryDetails(string pDisplayType, string pId)
        {
            return loInventoryDetail.getInventoryDetails(pDisplayType, pId);
        }

        [HttpGet]
        public DataTable getStockInventory(string pSearchString)
        {
            return loInventoryDetail.getStockInventory(pSearchString);
        }

        [HttpGet]
        public DataTable getStockInventoryByLocation(string pLocationId, string pSearchString)
        {
            return loInventoryDetail.getStockInventoryByLocation(pLocationId, pSearchString);
        }

        [HttpGet]
        public DataTable getStockInventoryList(string pLocationId, string pSearchString)
        {
            return loInventoryDetail.getStockInventoryList(pLocationId, pSearchString);
        }

        [HttpPost]
        public bool insertInventoryDetail([FromBody]InventoryDetail pInventoryDetail)
        {
            return loInventoryDetail.insertInventoryDetail(pInventoryDetail);
        }

        [HttpPost]
        public bool updateInventoryDetail([FromBody]InventoryDetail pInventoryDetail)
        {
            return loInventoryDetail.updateInventoryDetail(pInventoryDetail);
        }

        [HttpGet]
        public bool removeInventoryDetail(string pDetailId, string pUserId)
        {
            return loInventoryDetail.removeInventoryDetail(pDetailId, pUserId);
        }
        #endregion ""
        #endregion

        #region "POS"
        #region "Cashier"
        [HttpGet]
        public DataTable getCashiers(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loCashier.getCashiers(pDisplayType, pPrimaryKey, pSearchString);
        }

        [HttpGet]
        public DataTable getCashierDetails(string pUserId)
        {
            return loCashier.getCashierDetails(pUserId);
        }

        [HttpPost]
        public string insertCashier([FromBody]Cashier pCashier)
        {
            return loCashier.insertCashier(pCashier);
        }

        [HttpPost]
        public string updateCashier([FromBody]Cashier pCashier)
        {
            return loCashier.updateCashier(pCashier);
        }

        [HttpGet]
        public bool removeCashier(string pId, string pUserId)
        {
            return loCashier.removeCashier(pId, pUserId);
        }
        #endregion ""

        #region "Mode of Payment"
        [HttpGet]
        public DataTable getModeOfPayments(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loModeOfPayment.getModeOfPayments(pDisplayType, pPrimaryKey, pSearchString);
        }

        [HttpGet]
        public DataTable getModeOfPaymentDefault()
        {
            return loModeOfPayment.getModeOfPaymentDefault();
        }

        [HttpPost]
        public string insertModeOfPayment([FromBody]ModeOfPayment pModeOfPayment)
        {
            return loModeOfPayment.insertModeOfPayment(pModeOfPayment);
        }

        [HttpPost]
        public string updateModeOfPayment([FromBody]ModeOfPayment pModeOfPayment)
        {
            return loModeOfPayment.updateModeOfPayment(pModeOfPayment);
        }

        [HttpGet]
        public bool removeModeOfPayment(string pId, string pUserId)
        {
            return loModeOfPayment.removeModeOfPayment(pId, pUserId);
        }
        #endregion ""

        #region "Discount"
        [HttpGet]
        public DataTable getDiscounts(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loDiscount.getDiscounts(pDisplayType, pPrimaryKey, pSearchString);
        }

        [HttpPost]
        public string insertDiscount([FromBody]Discount pDiscount)
        {
            return loDiscount.insertDiscount(pDiscount);
        }

        [HttpPost]
        public string updateDiscount([FromBody]Discount pDiscount)
        {
            return loDiscount.updateDiscount(pDiscount);
        }

        [HttpGet]
        public bool removeDiscount(string pId, string pUserId)
        {
            return loDiscount.removeDiscount(pId, pUserId);
        }
        #endregion

        #region "Cashier Period"
        [HttpGet]
        public DataTable getCashierPeriods(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loCashierPeriod.getCashierPeriods(pDisplayType, pPrimaryKey, pSearchString);
        }

        [HttpGet]
        public DataTable getCashierPeriodOpen()
        {
            return loCashierPeriod.getCashierPeriodOpen();
        }

        [HttpGet]
        public DataTable getCashierPeriodStockSold(string pCashierPeriodId)
        {
            return loCashierPeriod.getCashierPeriodStockSold(pCashierPeriodId);
        }

        [HttpGet]
        public DataTable getCashierPeriodReturnedItem(string pCashierPeriodId)
        {
            return loCashierPeriod.getCashierPeriodReturnedItem(pCashierPeriodId);
        }

        [HttpGet]
        public DataTable getCashierPeriodByDate(DateTime pStartDate, DateTime pEndDate)
        {
            return loCashierPeriod.getCashierPeriodByDate(pStartDate, pEndDate);
        }

        [HttpPost]
        public string insertCashierPeriod([FromBody]CashierPeriod pCashierPeriod)
        {
            return loCashierPeriod.insertCashierPeriod(pCashierPeriod);
        }

        [HttpPost]
        public string updateCashierPeriod([FromBody]CashierPeriod pCashierPeriod)
        {
            return loCashierPeriod.updateCashierPeriod(pCashierPeriod);
        }

        [HttpGet]
        public bool openCashierPeriod(string pCashierId, decimal pCashDeposit, string pRemarks, string pUserId)
        {
            return loCashierPeriod.openCashierPeriod(pCashierId, pCashDeposit, pRemarks, pUserId);
        }

        [HttpGet]
        public bool closeCashierPeriod(string pId, string pCashierId, decimal pTotalSales, decimal pReturnedItemTotal,
            decimal pTotalDiscount, decimal pNetSales, decimal pNonCashSales,
            decimal pCashSales, decimal pNetCashSales, decimal pCashCount, decimal pVariance,
            string pRemarks, string pUserId)
        {
            return loCashierPeriod.closeCashierPeriod(pId, pCashierId, pTotalSales, pReturnedItemTotal,
                pTotalDiscount, pNetSales, pNonCashSales,
                pCashSales, pNetCashSales, pCashCount, pVariance, pRemarks, pUserId);
        }

        [HttpGet]
        public bool removeCashierPeriod(string pId, string pUserId)
        {
            return loCashierPeriod.removeCashierPeriod(pId, pUserId);
        }
        #endregion ""

        #region "POS Transaction"
        [HttpGet]
        public DataTable getPOSTransactions(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loPOSTransaction.getPOSTransactions(pDisplayType, pPrimaryKey, pSearchString);
        }

        [HttpGet]
        public DataTable getPOSTransactionsByDate(DateTime pStartDate, DateTime pEndDate)
        {
            return loPOSTransaction.getPOSTransactionsByDate(pStartDate, pEndDate);
        }

        [HttpGet]
        public DataTable getPOSTransaction(string pId)
        {
            return loPOSTransaction.getPOSTransaction(pId);
        }

        [HttpGet]
        public DataTable getPOSTransactionLists(string pCashierPeriodId)
        {
            return loPOSTransaction.getPOSTransactionLists(pCashierPeriodId);
        }

        [HttpGet]
        public DataTable getTotalSalesByCashierPeriod(string pCashierPeriodId)
        {
            return loPOSTransaction.getTotalSalesByCashierPeriod(pCashierPeriodId);
        }

        [HttpGet]
        public DataTable getTotalReturnedByCashierPeriod(string pCashierPeriodId)
        {
            return loPOSTransaction.getTotalReturnedByCashierPeriod(pCashierPeriodId);
        }

        [HttpGet]
        public DataTable getTotalDiscountByCashierPeriod(string pCashierPeriodId)
        {
            return loPOSTransaction.getTotalDiscountByCashierPeriod(pCashierPeriodId);
        }

        [HttpGet]
        public DataTable getSalesByCashierPeriod(string pCashierPeriodId)
        {
            return loPOSTransaction.getSalesByCashierPeriod(pCashierPeriodId);
        }

        [HttpPost]
        public string insertPOSTransaction([FromBody]POSTransaction pPOSTransaction)
        {
            return loPOSTransaction.insertPOSTransaction(pPOSTransaction);
        }

        [HttpPost]
        public string updatePOSTransaction([FromBody]POSTransaction pPOSTransaction)
        {
            return loPOSTransaction.updatePOSTransaction(pPOSTransaction);
        }

        [HttpGet]
        public bool removePOSTransaction(string pId, string pUserId)
        {
            return loPOSTransaction.removePOSTransaction(pId, pUserId);
        }
        #endregion ""

        #region "POS Transaction Detail"
        [HttpGet]
        public DataTable getPOSTransactionDetails(string pTransactionId)
        {
            return loPOSTransactionDetail.getPOSTransactionDetails(pTransactionId);
        }

        [HttpGet]
        public DataTable getPOSTransactionDetailsForEdit(string pTransactionId)
        {
            return loPOSTransactionDetail.getPOSTransactionDetailsForEdit(pTransactionId);
        }

        [HttpGet]
        public DataTable getSalesInventory(DateTime pStartDate, DateTime pEndDate)
        {
            return loPOSTransactionDetail.getSalesInventory(pStartDate, pEndDate);
        }

        [HttpGet]
        public DataTable getSalesInventoryBy(DateTime pStartDate, DateTime pEndDate)
        {
            return loPOSTransactionDetail.getSalesInventoryBy(pStartDate, pEndDate);
        }

        [HttpGet]
        public DataTable getReturnedItems(DateTime pStartDate, DateTime pEndDate)
        {
            return loPOSTransactionDetail.getReturnedItems(pStartDate, pEndDate);
        }

        [HttpPost]
        public bool insertPOSTransactionDetail([FromBody]POSTransactionDetail pPOSTransactionDetail)
        {
            return loPOSTransactionDetail.insertPOSTransactionDetail(pPOSTransactionDetail);
        }

        [HttpPost]
        public bool updatePOSTransactionDetail([FromBody]POSTransactionDetail pPOSTransactionDetail)
        {
            return loPOSTransactionDetail.updatePOSTransactionDetail(pPOSTransactionDetail);
        }

        [HttpGet]
        public bool removePOSTransactionDetail(string pDetailId, string pUserId)
        {
            return loPOSTransactionDetail.removePOSTransactionDetail(pDetailId, pUserId);
        }
        #endregion ""

        #region "Returned Item"
        /*
        [HttpGet]
        public DataTable getReturnedItems(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loReturnedItem.getReturnedItems(pDisplayType,pPrimaryKey, pSearchString);
        }
        */
        [HttpPost]
        public string insertReturnedItem([FromBody]ReturnedItem pReturnedItem)
        {
            return loReturnedItem.insertReturnedItem(pReturnedItem);
        }

        [HttpPost]
        public string updateReturnedItem([FromBody]ReturnedItem pReturnedItem)
        {
            return loReturnedItem.updateReturnedItem(pReturnedItem);
        }

        [HttpGet]
        public bool removeReturnedItem(string pId, string pUserId)
        {
            return loReturnedItem.removeReturnedItem(pId, pUserId);
        }
        #endregion ""
        #endregion

        #region "SYSTEMS"
        #region "Users"
        [HttpGet]
        public DataTable authenticateUser(string pUsername, string pPassword)
        {
            if (pPassword == null)
            {
                pPassword = "";
            }
            return loUser.authenticateUser(pUsername, pPassword);
        }

        [HttpGet]
        public DataTable getUsers(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loUser.getUsers(pDisplayType, pPrimaryKey, pSearchString);
        }

        [HttpGet]
        public bool checkUserPassword(string pUserId, string pCurrentPassword)
        {
            return loUser.checkUserPassword(pUserId, pCurrentPassword);
        }

        [HttpGet]
        public bool changePassword(string pUserId,string pNewPassword)
        {
            return loUser.changePassword(pUserId, pNewPassword);
        }

        [HttpPost]
        public string insertUser([FromBody]User pUser)
        {
            return loUser.insertUser(pUser);
        }

        [HttpPost]
        public string updateUser([FromBody]User pUser)
        {
            return loUser.updateUser(pUser);
        }

        [HttpGet]
        public bool removeUser(string pId,string pUserId)
        {
            return loUser.removeUser(pId, pUserId);
        }
        #endregion ""

        #region "User Groups"
        [HttpGet]
        public DataTable getUserGroups(string pDisplayType, string pPrimaryKey, string pSearchString)
        {
            return loUserGroup.getUserGroups(pDisplayType,pPrimaryKey, pSearchString);
        }

        [HttpPost]
        public string insertUserGroup([FromBody]UserGroup pUserGroup)
        {
            return loUserGroup.insertUserGroup(pUserGroup);
        }

        [HttpPost]
        public string updateUserGroup([FromBody]UserGroup pUserGroup)
        {
            return loUserGroup.updateUserGroup(pUserGroup);
        }

        [HttpGet]
        public bool removeUserGroup(string pId, string pUserId)
        {
            return loUserGroup.removeUserGroup(pId, pUserId);
        }

        [HttpGet]
        public bool removeAllUserGroup(string pUserGroupId)
        {
            return loUserGroup.removeAllUserGroup(pUserGroupId);
        }

        [HttpGet]
        public bool updateUserGroupMenuItems(string pUserGroupId, string pMenuItem, string pItemName)
        {
            return loUserGroup.updateUserGroupMenuItems(pUserGroupId, pMenuItem, pItemName);
        }

        [HttpGet]
        public bool removeAllRights(string pUserGroupId, string pItemName)
        {
            return loUserGroup.removeAllRights(pUserGroupId, pItemName);
        }

        [HttpGet]
        public bool updateUserGroupRights(string pUserGroupId, string pItemName, string pRights)
        {
            return loUserGroup.updateUserGroupRights(pUserGroupId, pItemName, pRights);
        }
        #endregion ""

        #region "System Configurations"
        [HttpGet]
        public DataTable getSystemConfigurations()
        {
            return loSystemConfigurations.getSystemConfigurations();
        }

        [HttpPost]
        public bool updateSystemConfiguration([FromBody]SystemConfiguration pSystemConfiguration)
        {
            return loSystemConfigurations.updateSystemConfiguration(pSystemConfiguration);
        }
        #endregion ""

        #region "Audit Trail"
        [HttpGet]
        public DataTable getAuditTrailByDate(DateTime pFrom, DateTime pTo)
        {
            return loAuditTrail.getAuditTrailByDate(pFrom, pTo);
        }

        [HttpGet]
        public bool removeAuditTrail(DateTime pFrom, DateTime pTo)
        {
            return loAuditTrail.removeAuditTrail(pFrom, pTo);
        }
        #endregion ""

        #region "Backup / Restore Database"
        [HttpGet]
        public bool backupDatabase(string pSaveFileTo, string pBackupMySqlDumpAddress,
            string pUserId, string pPassword, string pServer, string pDatabase)
        {
            return loCommon.backupDatabase(pSaveFileTo, pBackupMySqlDumpAddress, pUserId, pPassword, pServer, pDatabase);
        }

        [HttpGet]
        public bool restoreDatabase(string pSQLFileFrom, string pRestoreMySqlAddress,
            string pUserId, string pPassword, string pServer, string pDatabase)
        {
            return loCommon.restoreDatabase(pSQLFileFrom, pRestoreMySqlAddress, pUserId, pPassword, pServer, pDatabase);
        }

        #endregion ""

        #endregion
    }
}
