export class Product {
  productID!: number;
  name!: string;
  description!: string;
  price!: number;
  typeID!: number;
  subtypeID!: number;
}
export class ProductDto {
  name!: string;
  description!: string;
  price!: number;
  type!: string;
  subtype!: string;
}
