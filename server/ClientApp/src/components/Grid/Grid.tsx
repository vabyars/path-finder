import React, {useState} from 'react'
import Cell from '../Ceil/Cell'
import {CellData, GridProps} from '../Extentions/Interfaces'
import {CellState} from "../Extentions/CellState";
import {UNCLICKABLE_CELL_TYPES} from "../Extentions/Constants";
import "./Grid.css"

function Grid(props: GridProps) {
    const [isMouseDown, setIsMouseDown] = useState(false)
    const [isStartSelect, setIsStartSelect] = useState(false)
    const [isEndSelect, setIsEndSelect] = useState(false)
    const width = props.columns * 22;
    const height = props.rows * 22;
    let rowsArr: any = [];

    function setStartOrEndEnable(cellData: CellData) {
        if (cellData.state === 'start') {
            setIsStartSelect(true)
        }

        if (cellData.state === 'end')
            setIsEndSelect(true)
    }


    for (let i = 0; i < props.field.length; i++) {
        let temp = []
        for (let j = 0; j < props.field[i].length; j++) {
            let boxId = `${i}_${j}`;
            temp.push(
                <Cell
                    key={boxId}
                    color={props.field[i][j].mainColor}
                    className={getCellClass(props.field[i][j].state)}
                    onMouseDown={() => {
                        setIsMouseDown(true)
                        if (UNCLICKABLE_CELL_TYPES.includes(props.field[i][j].state)) {
                            setStartOrEndEnable(props.field[i][j])
                            return
                        }

                        props.func(i, j, getNewCellDataOnClick(props.field[i][j], isStartSelect, isEndSelect))
                    }}
                    onMouseLeave={() => {
                        if ((isEndSelect && props.field[i][j].state === 'end')
                            || (isStartSelect && props.field[i][j].state === 'start')){
                          // props.printStartAndEnd()
                          props.func(i, j, {state: 'empty', value: 1, mainColor: "white"})

                        }


                    }
                    }
                    onMouseOver={() => {
                        if (UNCLICKABLE_CELL_TYPES.includes(props.field[i][j].state)) {
                          // props.printStartAndEnd()
                          return
                        }
                        if (isMouseDown)
                            props.func(i, j, getNewCellDataOnClick(props.field[i][j], isStartSelect, isEndSelect))
                    }}
                />)
        }
        rowsArr.push(temp)
    }

    return (
        <div className="grid"
             style={{width: width, height: height}}
             onMouseUp={() => {
               props.printStartAndEnd()
                 setIsMouseDown(false)
                 setIsEndSelect(false)
                 setIsStartSelect(false)
             }}
             onMouseLeave={(e) => {
               props.printStartAndEnd()
               setIsMouseDown(false)
               setIsEndSelect(false)
               setIsStartSelect(false)
             }}
        >
            {rowsArr}
        </div>
    );
}

function getNewCellDataOnClick(cellData: CellData, start: boolean, end: boolean) {
    if (start)
        return {
            mainColor: 'red',
            state: 'start',
            value: 1
        }
    if (end)
        return {
            mainColor: '#19c43c',
            state: 'end',
            value: 1
        }
    if (cellData.state === 'wall')
        return {
            mainColor: "white",
            state: 'empty',
            value: 1
        }

    return {
        mainColor: "black",
        state: 'wall',
        value: -1
    }
}

function getCellClass(state: CellState) {
    let additionalClass = state === 'empty' ? '' : state
    return `cell ${additionalClass}`
}

export default Grid