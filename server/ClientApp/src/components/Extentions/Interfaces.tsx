import {CellState} from "./CellState";

export interface CellData {
  state: CellState
  value: number,
  mainColor: string
}

export interface GridProps {
  rows: number,
  columns: number
  field: CellData[][]
  func: any
  printStartAndEnd: any
}

export interface CellIndex {
  x: number,
  y: number
}

export interface CeilProps {
  className: string
  onMouseDown: any
  onMouseOver: any
  onMouseLeave: any
  color: string
}

export interface Field {
  start: CellIndex
  end: CellIndex
  field: CellData[][]
}

export interface HeaderProps {
  initField:(rows: number, columns: number) =>  void,
  clearField: () => void,
  executeAlgorithm: (name: string) => void,
  clearPath: () => void,
  setPrebuildField: (field: Field) => void
  saveMaze: (name: string) => Promise<any>
}



export interface SelectData{
  label: string,
  value: number
}