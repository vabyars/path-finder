import React, {useEffect, useState} from 'react'
import {Button, Input, Modal} from '@skbkontur/react-ui'
import './Header.css'
import Select from 'react-select';
import {CellData, HeaderProps, SelectData} from "../Extentions/Interfaces";
import {getCellIndex, parseCellsDataToNumbers} from "../Extentions/Functions";


function parseArrayToSelectData(data: any[]) {
  return data.map(function (label, i) {
    return {label: label, value: i + 1}
  })
}

function Header(props: HeaderProps) {
  const [algorithms, setAlgorithms] = useState<SelectData[]>([])
  const [currentAlgorithm, setAlgorithm] = useState<SelectData>()
  const [mazes, setMazes] = useState<SelectData[]>([])
  const [currentMaze, setMaze] = useState<SelectData>()
  const [isModalOpen, setIsModalOpen] = useState(false)


  async function loadMazesAndAlgorithms() {
    let data = await (await fetch("/settings")).json()
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
        <Button className="flex-elem" onClick={props.clearField}> Clear field</Button>
        <Button className="flex-elem" onClick={props.clearPath}> Clear path</Button>

        {currentAlgorithm && <Select className="flex-elem algorithm" isSearchable={false}
                                     options={algorithms} defaultValue={currentAlgorithm}
                                     onChange={(value: any) => setAlgorithm(value)}/>}
        {currentMaze && <Select className="flex-elem mazes" isSearchable={false}
                                options={mazes} defaultValue={currentMaze} onChange={(value: any) => {
          setMaze(value)
          fetch(`/maze/${value.label}`)
              .then((res) => res.json()
                  .then((data) => {
                    let maze = data.maze
                    let field: CellData[][] = []
                    for (let i = 0; i < maze.length; i++) {
                      for (let j = 0; j < maze[i].length; j++) {
                        if (!field[i])
                          field[i] = []
                        field[i][j] = {
                          value: maze[i][j],
                          state: maze[i][j] === -1 ? 'wall' : 'empty',
                          mainColor: maze[i][j] === -1 ? "black" : "white"
                        }
                      }
                    }
                    let start = getCellIndex(data.start)
                    let end = getCellIndex(data.end)

                    field[start.x][start.y] = {state: "start", mainColor: "red", value: 1}
                    field[end.x][end.y] = {state: "end", mainColor: "#19c43c", value: 1}
                    props.setPrebuildField({field: field,
                      start: start,
                      end: end})
                  })
                  .catch((e) => console.log(e)))
              .catch((e) => console.log(e))
        }}/>}

        <Button className="flex-elem" onClick={() => setIsModalOpen(true)}> Save maze</Button>
        {isModalOpen && <MyModal saveMaze={props.saveMaze} onClose={setIsModalOpen}/>}
      </div>
  )
}


function MyModal(props: any) {
  const [errorText, setErrorText] = useState("")
  const [isSaved, setIsSaved] = useState(false)
  const [name, setName] = useState("")

  return (
      <Modal onClose={() => props.onClose()}>
        <Modal.Header>Save your maze</Modal.Header>
        <Modal.Body>
          {isSaved ? <label className='content-item modal-header'>Saved</label>
              : <div className='modal-body-content'>

                <label className='content-item modal-header'>Enter the name of the maze</label>
                <label className='content-item subtitle'>{errorText}</label>
                <Input onChange={(event) => setName(event.target.value)}/>
              </div>}

        </Modal.Body>
        <Modal.Footer>
          {!isSaved && <Button use="pay" size="medium" onClick={() => {
            let prom: Promise<any> = props.saveMaze(name)
            prom.then((res) => {
                  if (res.ok)
                    setIsSaved(true)
                  else {
                    res.json().then((data: any) => {
                      setErrorText(data.errors.Name.join(". "))
                    }).catch((e: any) => console.log(e))
                  }

                }
            ).catch((e) => console.log(e))
          }}>Save</Button>}
        </Modal.Footer>
      </Modal>
  );
}

export default Header