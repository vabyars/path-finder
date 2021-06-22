import React, { useEffect, useState } from 'react'
import {Button, Input, Modal} from '@skbkontur/react-ui'
import './Header.css'
import Select from 'react-select';
import {CellData, HeaderProps, SelectData} from "../Extentions/Interfaces";




function parseArrayToSelectData(data: any[]){
    return data.map(function(label, i){return {label: label, value: i + 1}})
}

function Header(props: HeaderProps){
    const [algorithms, setAlgorithms] = useState<SelectData[]>([])
    const [currentAlgorithm, setAlgorithm] = useState<SelectData>()
    const [mazes, setMazes] = useState<SelectData[]>([])
    const [currentMaze, setMaze] = useState<SelectData>()
    const [isModalOpen, setIsModalOpen] = useState(false)
    const [name, setName] = useState("")

  async function loadMazesAndAlgorithms(){
    let data = await( await fetch("/settings")).json()
    let parsedAlgorithms = parseArrayToSelectData(data.algorithms)
    let parsedMazes = parseArrayToSelectData(data.mazes)
    props.initField(data.height, data.width)
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
            <Button className="flex-elem" onClick={() => {
              if (currentAlgorithm)
                props.executeAlgorithm(currentAlgorithm.label)
            }}> Start</Button>
            <Button className="flex-elem" onClick={() => console.log(algorithms)} > Pause</Button>
            <Button className="flex-elem" onClick={props.clearField}> Clear field</Button>
            <Button className="flex-elem" onClick={props.clearPath}> Clear path</Button>

          {currentAlgorithm && <Select className="flex-elem algorithm" isSearchable={false}
              options={algorithms} defaultValue={currentAlgorithm}  onChange={(value: any) => setAlgorithm(value)} />}
          {currentMaze &&  <Select className="flex-elem mazes" isSearchable={false}
              options={mazes} defaultValue={currentMaze}  onChange={(value: any) =>
            {
              setMaze(value)
              fetch(`/maze/${value.label}`)
                  .then((res) => res.json()
                      .then((data: number[][]) => {
                        console.log(data)
                        let field: CellData[][] = []
                        for (let i = 0; i < data.length; i++) {
                          for (let j = 0; j < data[i].length; j++) {
                            if (!field[i])
                              field[i] = []
                            field[i][j] = {
                              value: data[i][j],
                              state: data[i][j] === -1 ? 'wall' : 'empty',
                              mainColor: data[i][j] === -1 ? "black" : "white"
                            }
                          }
                        }
                        props.setPrebuildField(field)
                      })
                      .catch((e) => console.log(e)))
                  .catch((e) => console.log(e))
            }} />}

          <Button className="flex-elem" onClick={() => setIsModalOpen(true)}> Save maze</Button>
          {isModalOpen && MyModal(props.saveMaze,() => setIsModalOpen(false), setName, () => name)}
        </div>        
    )
}


function MyModal(saveMaze: (name: string) => void, onClose: () => void, setName: any, name: () => string ){

  return (
      <Modal onClose={() => onClose()} >
        <Modal.Header>User has been saved</Modal.Header>
        <Modal.Body>
          <label>Введите название</label>
          <Input onChange={(event) =>  setName(event.target.value)}/>

        </Modal.Body>
        <Modal.Footer>
          <Button use="pay" size="medium" onClick={() => {
            saveMaze(name())
          }}>Save</Button>
        </Modal.Footer>
      </Modal>
  );
}


export default Header