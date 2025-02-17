import { Product } from "@/types/product";
import ProductCard from "@/components/shared/product/product-card";

type Props = {
  data: Product[];
  limit?: number;
  title?: string;
};

export default function ProductList({ data, title, limit }: Props) {
  const limitedData = limit && data.length >= limit
      ? data.slice(0, limit)
      : data;

  return (
    <div className="my-10">
      <h2 className="h2-bold mb-4">{title}</h2>
      { limitedData.length > 0 ? (
        <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4">
          { limitedData.map((product, index) => (
              <ProductCard key={index} product={product} />
          ))}
        </div>
      ) : (
        <div>
          <p>No products found.</p>
        </div>
      ) }
    </div>
  );

}