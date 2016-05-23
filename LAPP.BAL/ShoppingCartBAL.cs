using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using LAPP.ENTITY;
using LAPP.DAL;
namespace LAPP.BAL
{
    public class ShoppingCartBAL : BaseBAL
    {
        ShoppingCartDAL objDal = new ShoppingCartDAL();

        public int Save_ShoppingCart(ShoppingCart objShoppingCart)
        {
            return objDal.Save_ShoppingCart(objShoppingCart);
        }

        public List<ShoppingCart> Get_All_ShoppingCart()
        {
            return objDal.Get_All_ShoppingCart();
        }

        public List<ShoppingCart> Get_ShoppingCart_by_IndividualId(int IndividualId)
        {
            return objDal.Get_ShoppingCart_by_IndividualId(IndividualId);
        }

        public ShoppingCart Get_ShoppingCart_By_ShoppingCartId(int ID)
        {
            return objDal.Get_ShoppingCart_By_ShoppingCartId(ID);
        }

    }
}
