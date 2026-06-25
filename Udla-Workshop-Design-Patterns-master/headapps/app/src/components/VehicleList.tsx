import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { VehicleService } from "@/lib/services/vehicle";
import { Vehicle } from "@/lib/types/vehicle";

export const VehicleList = async () => {
  async function getVehicles(): Promise<Vehicle[]> {
    const service = new VehicleService();
    return service.all();
  }

  const vehicles = await getVehicles();
  return (
    <Table>
      <TableHeader>
        <TableRow>
          <TableHead>ID</TableHead>
          <TableHead>Brand</TableHead>
          <TableHead>Model</TableHead>
          <TableHead>Year</TableHead>
        </TableRow>
      </TableHeader>
      <TableBody>
        {vehicles.map((vehicle) => (
          <TableRow key={vehicle.id}>
            <TableCell>{vehicle.id}</TableCell>
            <TableCell>{vehicle.brand}</TableCell>
            <TableCell>{vehicle.model}</TableCell>
            <TableCell>{vehicle.year}</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
};
