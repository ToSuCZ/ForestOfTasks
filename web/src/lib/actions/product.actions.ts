'use server';

import { prisma } from "@/db/prisma";
import { Product } from "@/types/product";
import { LATEST_PRODUCTS_LIMIT } from "@/lib/constants";

export async function getLatestProducts(): Promise<Product[]> {
  return prisma.product.findMany({
    orderBy: {
      createdAt: 'desc',
    },
    take: LATEST_PRODUCTS_LIMIT,
  });
}

export async function getProductBySlug(slug: string): Promise<Product | null> {
  return prisma.product.findFirst({
    where: {
      slug,
    },
  });
}