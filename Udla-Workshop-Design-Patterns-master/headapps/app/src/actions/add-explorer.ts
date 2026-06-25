"use server";

import { VehicleService } from "@/lib/services/vehicle";
import { Vehicle } from "@/lib/types/vehicle";
import { v4 as uuid } from "uuid";

export async function addExplorer() {
 const service = new VehicleService();
 service.add({
   id: uuid(),
   model: "Explorer",
   brand: "Ford",
   year: 2025
  } as Vehicle);
}

