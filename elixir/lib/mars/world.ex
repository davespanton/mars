defmodule Mars.World do
  @moduledoc """
  Responsible for checking whether positions can be entered by a robot.
  """
  alias Mars.Robot

  defstruct scents: MapSet.new,
            max_x: 0,
            max_y: 0

  @doc """
  Take a map and a robot and returns whether the robot is in a position that
  can be occupied.
  """
  def check(%__MODULE__{max_y: max}, %Robot{y: y}) when y > max,
  do: :lost
  def check(%__MODULE__{},           %Robot{y: y}) when y < 0,
  do: :lost
  def check(%__MODULE__{max_x: max}, %Robot{x: x}) when x > max,
  do: :lost
  def check(%__MODULE__{},           %Robot{x: x}) when x < 0,
  do: :lost

  def check(%__MODULE__{}, %Robot{}),
  do: :ok


  @doc """
  Checks whether a scent is present at a given coordinates
  """
  def scent?(%__MODULE__{} = world, %{x: x, y: y}) do
    world.scents
    |> MapSet.member?({x, y})
  end

  @doc """
  Add a scent to the given world at the supplied coordinates.
  """
  def put_scent(%__MODULE__{} = world, %{x: x, y: y}) do
    new_scents = world.scents |> MapSet.put({x, y})
    %{ world | scents: new_scents }
  end
end
