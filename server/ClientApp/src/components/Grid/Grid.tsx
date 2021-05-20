import React, { useState } from 'react'
import Cell from '../Ceil/Cell'
import { CellData, GridProps }  from '../Extentions/Interfaces'
import { CellState } from "../Extentions/CellState";
import { UNCLICKABLE_CELL_TYPES} from "../Extentions/Constants";
import "./Grid.css"

function Grid(props: GridProps){
    const [isMouseDown, setIsMouseDown] = useState(false)

    const width = props.columns * 22;
    const height = props.rows * 22;
    let rowsArr: any = [];


    
    for(let i = 0; i < props.rows; i++) {
        let temp = []
        for(let j = 0; j < props.columns; j++) {
            let boxId = `${i}_${j}`;
            temp.push(
                <Cell
                    key={boxId}
                    className={getCellClass(props.field[i][j].state)}
                    onMouseDown={() => {
                      if (UNCLICKABLE_CELL_TYPES.includes(props.field[i][j].state)) return
                      setIsMouseDown(true)
                      props.func(i, j, [getNewCellDataOnClick(props.field[i][j])])
                    }}
                    onMouseOver={() => {
                      if (UNCLICKABLE_CELL_TYPES.includes(props.field[i][j].state)) return
                      if (isMouseDown) props.func(i, j, [getNewCellDataOnClick(props.field[i][j])])
                    }}
                />)
        }
        rowsArr.push(temp)
    }

    return (
        <div className="grid"
             style={{width: width, height: height}}
             onMouseUp={() => {
               setIsMouseDown(false)
             }}
             onMouseLeave={(e) => {
               setIsMouseDown(false)
             }}
          >
          {rowsArr}
        </div>
    );
}

function getNewCellDataOnClick(cellData: CellData) {
  if (cellData.state === 'wall')
    return {
      state: 'empty',
      value: 0
    }
  return {
    state: 'wall',
    value: -1
  }
}

function getCellClass(state: CellState) {
  let additionalClass = state === 'empty' ? '' : state
  return `cell ${additionalClass}`
}

export default Grid