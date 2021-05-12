import React, { useState } from 'react'
import './Ceil.css'

interface CeilProps {
    className: string
    onMouseDown: any
    onMouseOver: any
    onMouseUp: any
}

function Ceil(props: CeilProps){
    
    return (
        <div 
            className={props.className }
            style={{width: 20, height: 20}}
            onMouseDown={props.onMouseDown}
            onMouseOver={props.onMouseOver}
            onMouseUp={props.onMouseUp}
        >
        </div>
    )
}

export default Ceil