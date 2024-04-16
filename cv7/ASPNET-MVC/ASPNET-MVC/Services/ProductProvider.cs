using ASPNET_MVC.Models;

namespace ASPNET_MVC.Services;

public class ProductProvider {
	public ProductProvider() {
	}

	public List<Product> List() {
		return Product.GetProducts();
	}
	public Product Find(int id) {
		return Product.GetProducts().FirstOrDefault(x=> x.Id == id);
	}
}