using CI3540.UI.Areas.Store.Models;

namespace CI3540.UI.Services
{
    public interface  ICartService
    {
        CartViewModel GetCartByCustomerId(int customerId);
        CartViewModel AddProductToCart(int customerId, int productId, int quantity);
        CartViewModel RemoveProductFromCart(int customerId, int productId, int quantity);
        CartViewModel DeleteProductFromCart(int customerId, int productId);
        CartViewModel GetCartByCartId(int cartId);
        CartViewModel GetCartByOrderLineId(int orderLineId);

    }
}