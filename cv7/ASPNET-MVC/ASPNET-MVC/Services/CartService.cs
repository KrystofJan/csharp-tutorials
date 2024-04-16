using ASPNET_MVC.Models;

namespace ASPNET_MVC.Services;

public class CartService {
	public CartService() {
	}

	public List<Product> _Products { get; set; } = new List<Product>();
	
	public void Add(Product product) {
		_Products.Add(product);	
	}

	public int Count {
		get { return _Products.Count; }
	}

}