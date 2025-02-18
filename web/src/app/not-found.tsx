'use client';

import Image from "next/image";
import {APP_NAME} from "@/lib/constants";
import { Button } from "@/components/ui/button";

export default function NotFound() {
    return (
      <div className="flex flex-col items-center justify-center min-h-screen">
        <Image
            src={'/images/logo.svg'}
            width={48}
            height={48}
            alt={`${APP_NAME} logo`}
            priority={true}
        />
        <div className="p-6 w-1/3 rounded-lg shadow-md text-center">
          <h1 className="text-3xl font-bold mb-4">404 - Page Not Found</h1>
          <p className="text-lg">The page you are looking for was not found</p>
          <Button variant="destructive" className="mt-4 ml-2" onClick={() => window.location.href = '/'}>Back to Homepage</Button>
        </div>
      </div>
    );
}