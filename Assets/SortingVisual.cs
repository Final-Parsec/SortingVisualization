using UnityEngine;
using System.Collections;

public class SortingVisual : MonoBehaviour {

	public float speed;

	public GameObject bar;

	public GameObject bubbleSortText;
	public GameObject insertionSortText;
	public GameObject selectionSortText;
	public GameObject quickSortText;


	private int[] bubbleArray = new int[100];
	private GameObject[] bubbleBars = new GameObject[100];

	private int[] insertionArray = new int[100];
	private GameObject[] insertionBars = new GameObject[100];

	private int[] selectionArray = new int[100];
	private GameObject[] selectionBars = new GameObject[100];

	private int[] quickArray = new int[100];
	private GameObject[] quickBars = new GameObject[100];

	// Use this for initialization
	void Start () {
		Vector3 leftScreenEdge = Camera.main.ScreenToWorldPoint(Vector3.zero);

		Instantiate(bubbleSortText, new Vector3(leftScreenEdge.x + 5, bubbleSortText.transform.position.y, bubbleSortText.transform.position.z), Quaternion.Euler(Vector3.zero));
		Instantiate(insertionSortText, new Vector3(leftScreenEdge.x + 5, insertionSortText.transform.position.y, insertionSortText.transform.position.z), Quaternion.Euler(Vector3.zero));
		Instantiate(selectionSortText, new Vector3(leftScreenEdge.x + 5, selectionSortText.transform.position.y, selectionSortText.transform.position.z), Quaternion.Euler(Vector3.zero));
		Instantiate(quickSortText, new Vector3(leftScreenEdge.x + 5, quickSortText.transform.position.y, quickSortText.transform.position.z), Quaternion.Euler(Vector3.zero));

		// Make arrays of randome numbers to be sorted
		for(int x=0; x<100; x++){

			bubbleArray[x] = UnityEngine.Random.Range(1,11);
			GameObject cube = Instantiate(bar, new Vector3(leftScreenEdge.x + 40 +(1.5f*x), bubbleSortText.transform.position.y - 2, bubbleSortText.transform.position.z), Quaternion.Euler(Vector3.zero)) as GameObject;
			cube.transform.localScale = new Vector3(cube.transform.localScale.x, bubbleArray[x], cube.transform.localScale.z);
			bubbleBars[x] = cube;


			insertionArray[x] = UnityEngine.Random.Range(1,11);
			cube = Instantiate(bar, new Vector3(leftScreenEdge.x + 40 +(1.5f*x), insertionSortText.transform.position.y - 2, insertionSortText.transform.position.z), Quaternion.Euler(Vector3.zero)) as GameObject;
			cube.transform.localScale = new Vector3(cube.transform.localScale.x, insertionArray[x], cube.transform.localScale.z);
			insertionBars[x] = cube;


			selectionArray[x] = UnityEngine.Random.Range(1,11);
			cube = Instantiate(bar, new Vector3(leftScreenEdge.x + 40 +(1.5f*x), selectionSortText.transform.position.y - 2, selectionSortText.transform.position.z), Quaternion.Euler(Vector3.zero)) as GameObject;
			cube.transform.localScale = new Vector3(cube.transform.localScale.x, selectionArray[x], cube.transform.localScale.z);
			selectionBars[x] = cube;


			quickArray[x] = UnityEngine.Random.Range(1,11);
			cube = Instantiate(bar, new Vector3(leftScreenEdge.x + 40 +(1.5f*x), quickSortText.transform.position.y - 2, quickSortText.transform.position.z), Quaternion.Euler(Vector3.zero)) as GameObject;
			cube.transform.localScale = new Vector3(cube.transform.localScale.x, quickArray[x], cube.transform.localScale.z);
			quickBars[x] = cube;

		}

		// Start sorting
		StartCoroutine(Bubble (bubbleArray));
		StartCoroutine(Insertion (insertionArray));
		StartCoroutine(Selection (selectionArray));
		StartCoroutine(Quicksort (quickArray, 0, quickArray.Length-1));
	}
	
	// Update is called once per frame
	void Update () {

		for(int x=0; x<100; x++){
			bubbleBars[x].transform.localScale = new Vector3(bubbleBars[x].transform.localScale.x, bubbleArray[x], bubbleBars[x].transform.localScale.z);
			insertionBars[x].transform.localScale = new Vector3(insertionBars[x].transform.localScale.x, insertionArray[x], insertionBars[x].transform.localScale.z);
			selectionBars[x].transform.localScale = new Vector3(selectionBars[x].transform.localScale.x, selectionArray[x], selectionBars[x].transform.localScale.z);
			quickBars[x].transform.localScale = new Vector3(quickBars[x].transform.localScale.x, quickArray[x], quickBars[x].transform.localScale.z);
		}
	}

	IEnumerator Selection(int[] arr){
		/* a[0] to a[n-1] is the array to sort */
		int i,j;
		int iMin;
		/* advance the position through the entire array */
		/*   (could do j < n-1 because single element is also min element) */
		for (j = 0; j < arr.Length-1; j++) {
			/* find the min element in the unsorted a[j .. n-1] */
			/* assume the min is the first element */
			iMin = j;
			/* test against elements after j to find the smallest */
			for ( i = j+1; i < arr.Length; i++) {
				/* if this element is less, then it is the new minimum */  
				if (arr[i] < arr[iMin]) {
					/* found new minimum; remember its index */
					iMin = i;

				}
			}
			if(iMin != j) {
				// swap
				int temp = arr[j];
				arr[j] = arr[iMin];
				arr[iMin] = temp;

				yield return new WaitForSeconds(speed);
			}
		}
	}

	IEnumerator Quicksort(int[] array, int i, int k){
		if (i < k-1){
			int left = i;
			int right = k;

			// Partition setment
			int pivotIndex = left + (right-left)/2;
			int pivotValue = array[pivotIndex];
			int temp = array[pivotIndex];
			array[pivotIndex] = array[right];
			array[right] = temp;
			int storeIndex = left;
			for (int a=left; a<right; a++){
				if (array[a] < pivotValue){
					temp = array[a];
					array[a] = array[storeIndex];
					array[storeIndex] = temp;
					storeIndex = storeIndex + 1;

					yield return new WaitForSeconds(speed);
				}
			}
			temp = array[storeIndex];
			array[storeIndex] = array[right];
			array[right] = temp;// Move pivot to its final place

			int p = storeIndex;

			StartCoroutine(Quicksort(array, i, p - 1));
			StartCoroutine(Quicksort(array, p+1, k));

		}
	}
	
	IEnumerator Insertion(int[] A){
		for (int i=0; i<A.Length; i++){
			int j = i;
			while (j > 0 && A[j-1] > A[j]){
				int temp = A[j];
				A[j] = A[j-1]; 
				A[j-1] = temp;
				j--;

				yield return new WaitForSeconds(speed);
			}
		}
	}

	IEnumerator Bubble(int[] A){
		int n = A.Length + 1;
		bool swapped = true;
		while (swapped){
			swapped = false;
			for(int i = 1; i<n-1; i++){
				if (A[i-1] > A[i]){
					int temp = A[i-1];
					A[i-1] = A[i];
					A[i] = temp;
					
					swapped = true;

					yield return new WaitForSeconds(speed);
				}
			}
			n--;
		}
	}
	
	private void Count(int[] input)
	{
		int[] range = {0,1,2,3,4,5,6,7,8,9};
		int[] count = new int[input.Length];
		int[] output = new int[input.Length];
		
		foreach(int x in input){
			count[x] += 1;
		}
		
		int total = 0;
		
		foreach(int i in range){
			int oldCount = count[i];
			count[i] = total;
			total += oldCount;
		}
		
		foreach(int x in input){
			output[count[x]] = x;
			count[x] += 1;
		}
		UnityEngine.Debug.Log("done");
	}
}
