import {ThemeEnum} from "@/constants/ThemeEnum";
import {DropdownMenuCheckboxItem} from "@/components/ui/dropdown-menu";
import {capitalizeFirstLetter} from "@/lib/utils";
import React from "react";
import {useTheme} from "next-themes";

type Props = {
  componentTheme: ThemeEnum
};

export default function DropdownMenuThemeItem({
  componentTheme
}: Props)
{
  const { theme, setTheme } = useTheme();

  return (
    <DropdownMenuCheckboxItem
      checked={theme == componentTheme}
      onClick={() => setTheme(componentTheme)}
    >
      {capitalizeFirstLetter(componentTheme)}
    </DropdownMenuCheckboxItem>
  )
}