import {CellData} from "./Interfaces";

export function parseCellsDataToNumbers(data: CellData[][]) {
  return data.map((row) => {
    return row.map((cellData) => cellData.value)
  })
}

export function getCellIndex(str: string) {
  let indexes = str.split(', ')
  return {x: parseInt(indexes[0]), y: parseInt(indexes[1])}
}