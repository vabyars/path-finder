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
    const width = props.columns * 22;
    const height = props.rows * 22;
    let rowsArr: any = [];



    for(let i = 0; i < props.rows; i++) {
        for(let j = 0; j < props.columns; j++) {
            let boxId = `${i}_${j}`;
            rowsArr.push(
                <Ceil
                    key={boxId}
                    isPaint={props.field[i][j]}                    
                    func={(value: boolean) => props.func(i, j, value)}
                />)
        }
    }

    return (
        <div className="grid" style={{width: width, height: height}}>
            {rowsArr}
        </div>
    );


}

export default Grid