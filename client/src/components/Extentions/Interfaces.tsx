import {CellState} from "./CellState";

export interface CellData {
  state: CellState
  value: number,
}

export interface GridProps {
  rows: number,
  columns: number
  field: CellData[][]
  func: any
}

interface CellIndex {
  x: number,
  y: number
}

export interface Field {
  start: CellIndex
  end: CellIndex
  field: CellData[][]

}