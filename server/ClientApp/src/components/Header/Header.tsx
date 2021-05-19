import React, { useEffect, useState } from 'react'
import { Button} from '@skbkontur/react-ui'
import './Header.css'
import Select from 'react-select';
import {CellData} from "../Extentions/Interfaces";


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

  async function loadMazesAndAlgorithms(){
    let data = await( await fetch("/settings")).json()
    let temp = data.algorithms.map((value:any) => value.name)
    let parsedAlgorithms = parseArrayToSelectData(temp)
    let parsedMazes = parseArrayToSelectData(data.mazes)

    setMazes(parsedMazes)
    setAlgorithms(parsedAlgorithms)
    setAlgorithm(parsedAlgorithms[0])
    setMaze(parsedMazes[0])
  }

  useEffect(() => {
    loadMazesAndAlgorithms()
  }, [])

    return (
        <div className="header">
            <label className="flex-elem logo">Pathfinder</label>
            <Button className="flex-elem" onClick={() => props.exec(currentAlgorithm?.label, props.field)}> Start</Button>
            <Button className="flex-elem"> Pause</Button>
            <Button className="flex-elem" onClick={props.clearFunc}> Clear field</Button>

            { currentAlgorithm && <Select className="flex-elem algorithm" isSearchable={false}
              options={algorithms} defaultValue={currentAlgorithm}  onChange={(value: any) => setAlgorithm(value)} />}
            { currentMaze && <Select className="flex-elem mazes" isSearchable={false}
              options={mazes} defaultValue={currentMaze}  onChange={(value: any) =>
            {
              setMaze(value)
              fetch(`/maze/${value.label}`)
                  .then((res) => res.json()
                      .then((data: number[][]) => {
                        let field: CellData[][] = []
                        for (let i = 0; i < data.length; i++) {
                          for (let j = 0; j < data[i].length; j++) {
                            if (!field[j])
                              field[j] = []
                            field[j][i] = {
                              value: data[i][j],
                              state: data[i][j] === -1 ? 'wall' : 'empty'
                            }
                          }
                      }
                        props.setField({...props.field, field: field})
                      })
                      .catch((e) => console.log(e)))
                  .catch((e) => console.log(e))
            }} /> }
        </div>        
    )
}

export default Header