import { Component } from '@angular/core';
import { Product, ProductDto } from 'src/shared/models/product';
import { AuthService } from 'src/shared/services/auth/authentication.service';
import { ProductService } from 'src/shared/services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent {
  products!: Product[];
  newProduct: ProductDto = new ProductDto();
  editId = -1;

  createNewProduct = false;

  constructor(private productService: ProductService, private authService: AuthService) { }


  ngOnInit() {
    this.productService.getProducts().subscribe(products => {
      this.products = products;
    });
  }
  
  addProduct(): void {
    this.productService.createProduct(this.newProduct).subscribe(product => {
      this.products.push(product);
      this.newProduct = new ProductDto();
    });
  }

  deleteProduct(id: number): void {
    this.productService.deleteProduct(id).subscribe(() => {
      this.products = this.products = this.products.filter(product => product.productID !== id);
    });
  }

  editClicked(id: number): void {
    this.editId = id;
    this.createNewProduct=true;
    var tmpprod = this.products.find(product => product.productID === id);
    this.newProduct.name = tmpprod?.name || '';
    this.newProduct.price = tmpprod?.price || 0;
    this.newProduct.description = tmpprod?.description || '';
    this
  }


  isManager(): boolean {
    return this.authService.isManager();
  }

}
