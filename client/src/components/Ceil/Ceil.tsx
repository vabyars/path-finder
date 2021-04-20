import React, { useState } from 'react'
import './Ceil.css'

interface CeilProps {
    isPaint: boolean
    func: any
}

function Ceil(props: CeilProps){    
    function handleClick() {
        props.func(!props.isPaint)
    }
    
    return (
        <div 
            className={props.isPaint ? "box on" : "box off"}
            style={{width: 20, height: 20}}
            onClick={handleClick}
        >
        </div>
    )
}

export default Ceil