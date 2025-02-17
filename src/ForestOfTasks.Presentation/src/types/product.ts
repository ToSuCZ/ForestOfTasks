import { z } from "zod";
import { productInsertSchema} from "@/lib/validators";

export type Product = z.infer<typeof productInsertSchema> & {
  id: string;
  createdAt: Date;
};

export type ProductsData = {
  products: Product[];
};