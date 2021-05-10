import React, { useState } from 'react'
import Ceil from '../Ceil/Ceil'
import "./Grid.css"

interface GridProps {
    rows: number,
    columns: number
    field: boolean[][]
    func: any
}



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
                <Ceil
                    key={boxId}
                    className={props.field[i][j] ? "box on" : "box off"}
                    onMouseDown={() => {
                      setIsMouseDown(true)
                      props.func(i, j, !props.field[i][j])
                    }}
                    onMouseOver={() => {
                      if (isMouseDown) props.func(i, j, !props.field[i][j])
                    }}
                    onMouseUp={() => setIsMouseDown(false)}
                />)
        }
        rowsArr.push(temp)
    }

    return (
        <div className="grid"
             style={{width: width, height: height}}
          >
          {rowsArr}
        </div>
    );
}

export default Grid