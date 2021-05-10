import React, { useEffect, useState } from 'react'
import { Button} from '@skbkontur/react-ui'
import './Header.css'
import { strict } from 'node:assert'
import Select from 'react-select';


interface SelectData{
    label: string,
    value: number
}

function parseArrayToSelectData(data: any[]){
    return data.map(function(label, i){return {label: label, value: i + 1}})
}


function Header(props: any){
    const [algorithms, setAlgorithms] = useState<SelectData[]>([])
    const [currentAlgorithm, setAlgorithm] = useState<SelectData>()
    const [mazes, setMazes] = useState<SelectData[]>([])
    const [currentMaze, setMaze] = useState<SelectData>()

    return (
        <div className="header">
            <label className="flex-elem logo">Pathfinder</label>

            <Button className="flex-elem" onClick={() => console.log(currentMaze)}> Start</Button>
            <Button className="flex-elem"> Pause</Button>
            <Button className="flex-elem" onClick={props.clearFunc}> Clear field</Button>

            { currentAlgorithm && <Select className="flex-elem algorithm" isSearchable={false}
              options={algorithms} defaultValue={currentAlgorithm}  onChange={(value: any) => setAlgorithm(value)} />}
            { currentMaze && <Select className="flex-elem mazes" isSearchable={false}
              options={mazes} defaultValue={currentMaze}  onChange={(value: any) => setMaze(value)} /> }
        </div>        
    )
}

export default Header