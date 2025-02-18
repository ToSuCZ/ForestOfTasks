import {cn} from "@/lib/utils";
import { Prisma } from "@prisma/client";

type Props = {
  price: string;
  className?: string;
};

export default function ProductPrice({ price, className }: Props) {
  const [integer, decimal] = new Prisma.Decimal(price).toFixed(2).split('.');

  return (
    <p className={cn('text-2xl', className)}>
      <span className="text-xs align-super">$</span>
      {integer}
      <span className="text-xs align-super">{decimal}</span>
    </p>
  );
}