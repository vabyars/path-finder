import React, { useState } from 'react'
import Cell from '../Ceil/Cell'
import { CellData, GridProps }  from '../Extentions/Interfaces'
import { CellState } from "../Extentions/CellState";
import { UNCLICKABLE_CELL_TYPES} from "../Extentions/Constants";
import "./Grid.css"

function Grid(props: GridProps){
    const [isMouseDown, setIsMouseDown] = useState(false)
    const [isStartSelect, setIsStartSelect] = useState(false)
    const [isEndSelect, setIsEndSelect] = useState(false)
    const width = props.columns * 22;
    const height = props.rows * 22;
    let rowsArr: any = [];

    function a(cellData: CellData) {
      if (cellData.state === 'start')
      {
        setIsStartSelect(true)
      }

      if (cellData.state === 'end')
        setIsEndSelect(true)
    }
    
    for(let i = 0; i < props.rows; i++) {
        let temp = []
        for(let j = 0; j < props.columns; j++) {
            let boxId = `${i}_${j}`;
            temp.push(
                <Cell
                    key={boxId}
                    className={getCellClass(props.field[i][j].state)}
                    onMouseDown={() => {
                      setIsMouseDown(true)
                      if (UNCLICKABLE_CELL_TYPES.includes(props.field[i][j].state)) {
                        a(props.field[i][j])
                        return}

                      props.func(i, j,  getNewCellDataOnClick(props.field[i][j], isStartSelect, isEndSelect))
                    }}
                    onMouseLeave={() => {
                      if ((isEndSelect && props.field[i][j].state === 'end')
                          || (isStartSelect && props.field[i][j].state === 'start'))
                        props.func(i, j,  {state: 'empty', value: 1})
                    }
                    }
                    onMouseOver={() => {
                      if (UNCLICKABLE_CELL_TYPES.includes(props.field[i][j].state)) return
                      if (isMouseDown)
                        props.func(i, j,  getNewCellDataOnClick(props.field[i][j], isStartSelect, isEndSelect))
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
               setIsEndSelect(false)
               setIsStartSelect(false)
             }}
             onMouseLeave={(e) => {
               setIsMouseDown(false)
             }}
          >
          {rowsArr}
        </div>
    );
}

function getNewCellDataOnClick(cellData: CellData, start: boolean, end: boolean) {
  if (start)
    return {
      state: 'start',
      value: 1
    }
  if (end)
    return {
      state: 'end',
      value: 1
    }
  if (cellData.state === 'wall')
    return {
      state: 'empty',
      value: 1
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