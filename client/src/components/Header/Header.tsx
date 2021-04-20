import React, { useState } from 'react'
import { Button} from '@skbkontur/react-ui'
import './Header.css'
import { strict } from 'node:assert'
// import Select from "react-dropdown-select";
import Select from 'react-select';


const algorithms =[
    { label: "A*", value: 1 },
    { label: "IDA*", value: 2 },
    { label: "Dijkstra", value: 3 },
    { label: "BFS", value: 4 },
    { label: "DFS", value: 5 },
]


function Header(props: any){
    const [currentAlgorithm, setAlgorithm] = useState(algorithms[0])

    return (
        <div className="header">
            <label className="flex-elem logo">Pathfinder</label>

            <Button className="flex-elem"> Start</Button>
            <Button className="flex-elem"> Pause</Button>
            <Button className="flex-elem" onClick={props.clearFunc}> Clear field</Button>

            <Select className="flex-elem algorithm" isSearchable={false}  options={algorithms} defaultValue={currentAlgorithm}  onChange={(value: any) => setAlgorithm(value)} />
        </div>        
    )
}

export default Header