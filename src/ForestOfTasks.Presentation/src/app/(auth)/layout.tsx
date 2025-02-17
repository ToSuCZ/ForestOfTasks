import {ReactNode} from "react";

export default function AuthLayout({ children }: { children: ReactNode }) {
  return (
    <div className="flex-center min-h-screen w-full">
      {children}
    </div>
  );

}