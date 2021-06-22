import React, { useState } from 'react'
import {CellProps} from "../Extentions/Interfaces";
import './Cell.css'




function Cell(props: CellProps){
    
    return (
        <div 
            className={props.className }
            style={{width: 20, height: 20, backgroundColor: props.color}}
            onMouseDown={props.onMouseDown}
            onMouseOver={props.onMouseOver}
            onMouseLeave={props.onMouseLeave}
        >
        </div>
    )
}

export default Cell