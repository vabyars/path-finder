import React, { useState } from 'react'
import {CeilProps} from "../Extentions/Interfaces";
import './Cell.css'




function Cell(props: CeilProps){
    
    return (
        <div 
            className={props.className }
            style={{width: 20, height: 20}}
            onMouseDown={props.onMouseDown}
            onMouseOver={props.onMouseOver}
        >
        </div>
    )
}

export default Cell