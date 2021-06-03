import {CellData} from "./Interfaces";

export function parseCellsDataToNumbers(data: CellData[][]) {
  return data.map((row) => {
    return row.map((cellData) => cellData.value)
  })
}