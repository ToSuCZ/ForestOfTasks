'use client';

import { useTheme } from 'next-themes';
import {
  DropdownMenu,
  DropdownMenuTrigger,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuContent,
} from "@/components/ui/dropdown-menu";
import { Button } from "@/components/ui/button";
import { SunMoon, MoonIcon, SunIcon } from "lucide-react";
import React, { useEffect } from "react";
import {ThemeEnum} from "@/constants/ThemeEnum";
import DropdownMenuThemeItem from "@/components/shared/header/dropdown-menu-theme-item";

export default function ModeToggle() {
  const [mounted, setMounted] = React.useState(false);
  const { theme } = useTheme();

  useEffect(() => setMounted(true), []);

  if (!mounted) return null;

  return (
      <DropdownMenu>
        <DropdownMenuTrigger asChild>
          <Button variant="ghost" className="focus-visible:ring-0 focus-visible:ring-offset-0">
            {theme === ThemeEnum.System ? (
                <SunMoon/>
            ) : theme === ThemeEnum.Dark ? (
                <MoonIcon/>
            ) : (
                <SunIcon/>
            )}
          </Button>
        </DropdownMenuTrigger>
        <DropdownMenuContent>
          <DropdownMenuLabel>Appearance</DropdownMenuLabel>
          <DropdownMenuSeparator/>
          <DropdownMenuThemeItem componentTheme={ThemeEnum.System} />
          <DropdownMenuThemeItem componentTheme={ThemeEnum.Dark} />
          <DropdownMenuThemeItem componentTheme={ThemeEnum.Light} />
        </DropdownMenuContent>
      </DropdownMenu>
  );
}